using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

using HistoryService.Contracts.HistoryRecordDTOs;
using HistoryService.Domain.Entities;
using HistoryService.Infrastructure.Data;

namespace HistoryService.Infrastructure.RabbitMQ;

public class RabbitMqListener : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly ILogger<RabbitMqListener> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _connectionString;

    public RabbitMqListener(
        IOptions<RabbitMqSettings> options,
        IConfiguration config,
        ILogger<RabbitMqListener> logger,
        IServiceProvider serviceProvider)
    {
        _settings = options.Value;
        _logger = logger;
        _serviceProvider = serviceProvider;
        _connectionString = config.GetConnectionString("messaging")!;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(_connectionString)
        };

        _connection = await factory.CreateConnectionAsync(stoppingToken);
        _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);

        await _channel.ExchangeDeclareAsync(
            exchange: _settings.HistoryExchange,
            type: ExchangeType.Fanout,
            durable: true
        );
        
        await _channel.QueueDeclareAsync(
            queue: _settings.HistoryQueue,
            durable: true,
            exclusive: false,
            autoDelete: false
        );

        await _channel.QueueBindAsync(
            queue: _settings.HistoryQueue,
            exchange: _settings.HistoryExchange,
            routingKey: ""
        );

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (_, ea) =>
        {
            try
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                var dto = JsonSerializer.Deserialize<HistoryRecordDTO>(message);

                if (dto != null)
                {
                    using var scope = _serviceProvider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<HistoryServiceDbContext>();

                    var entity = new HistoryRecord
                    {
                        HistoryEventType = dto.Type,
                        Date = dto.Date,
                        Payload = dto.Payload,
                        TriggeredBy = dto.TriggeredBy
                    };

                    db.HistoryRecords.Add(entity);
                    await db.SaveChangesAsync(stoppingToken);

                    _logger.LogInformation("Saved history record: {Type}", dto.Type);
                }

                await _channel.BasicAckAsync(ea.DeliveryTag, false, stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing history message");
                if (_channel != null)
                {
                    await _channel.BasicNackAsync(
                        ea.DeliveryTag,
                        multiple: false,
                        requeue: true,
                        cancellationToken: stoppingToken
                    );
                }
            }
        };

        await _channel.BasicConsumeAsync(
            queue: _settings.HistoryQueue,
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken
        );
    }
}
