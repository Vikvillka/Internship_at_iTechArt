using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

using HistoryService.Application.Intefaces.RabbitMQ;

namespace HistoryService.Infrastructure.RabbitMQ;

public class RabbitMqListener : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private readonly ILogger<RabbitMqListener> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _connectionString;
    private IConnection? _connection;
    private readonly List<IChannel> _channels = [];

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

        foreach (var queue in _settings.Queues)
        {
            var processorKey = queue.Key;
            var queueName = queue.Value;

            var channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
            _channels.Add(channel);

            await channel.ExchangeDeclareAsync(
                exchange: _settings.HistoryExchange,
                type: ExchangeType.Direct,
                durable: true
            );

            await channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            await channel.QueueBindAsync(
                queue: queueName,
                exchange: _settings.HistoryExchange,
                routingKey: queueName
            );

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (_, @event) =>
            {
                try
                {
                    var msg = Encoding.UTF8.GetString(@event.Body.ToArray());
                    _logger.LogInformation("Recieved history message: " + msg);

                    using var scope = _serviceProvider.CreateScope();
                    var processor = scope.ServiceProvider.GetKeyedService<IEventProcessor>(processorKey);

                    await processor.ProcessAsync(msg, stoppingToken);

                    await channel.BasicAckAsync(
                        @event.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing history message");
                    await channel.BasicNackAsync(
                        @event.DeliveryTag,
                        multiple: false,
                        requeue: true,
                        cancellationToken: stoppingToken
                    );
                }
            };

            await channel.BasicConsumeAsync(
                queue: queueName,
                autoAck: false,
                consumer: consumer,
                cancellationToken: stoppingToken
            );
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            foreach(var channel in _channels)
            {
                await channel.CloseAsync();
                channel.Dispose();
            }
            if (_connection != null)
            {
                await _connection.CloseAsync();
                _connection.Dispose();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error closing RabbitMQ resources");
        }
        await base.StopAsync(cancellationToken);
    }
}
