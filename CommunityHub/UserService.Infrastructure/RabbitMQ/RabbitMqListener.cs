using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

using UserService.Application.Intarfaces.RabbitMQ;

namespace UserService.Infrastructure.RabbitMQ;

public class RabbitMqListener : BackgroundService
{
    private readonly ILogger<RabbitMqListener> _logger;
    private IConnection? _connection;
    private readonly IServiceProvider _serviceProvider;
    private IChannel? _channel;
    private readonly RabbitMqSettings _settings;
    private readonly RabbitMqListenerOptions _map;
    private readonly string _connectionString;

    public RabbitMqListener(
        ILogger<RabbitMqListener> logger,
        IConfiguration config,
        IServiceProvider serviceProvider,
        IOptions<RabbitMqSettings> settings,
        IOptions<RabbitMqListenerOptions> map)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _connectionString = config.GetConnectionString("messaging")!;
        _settings = settings.Value;
        _map = map.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var _factory = new ConnectionFactory()
        {
            Uri = new Uri(_connectionString)
        };

        _connection = await _factory.CreateConnectionAsync(stoppingToken);

        foreach (var processor in _map.QueueMappings)
        {
            var processorType = processor.Key;
            var queueKey = processor.Value;
            var queueName = _settings.Queues[queueKey];
            var exchangeName = _settings.DeleteEntityExchange;

            _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
            
            await _channel.ExchangeDeclareAsync(
                exchange: exchangeName,
                type: ExchangeType.Direct,
                durable: true
            );

            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
            );

            await _channel.QueueBindAsync(
               queue: queueName,
               exchange: exchangeName,
               routingKey: queueName
            );

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, @event) =>
            {
                try
                {
                    var msg = Encoding.UTF8.GetString(@event.Body.ToArray());
                    _logger.LogInformation("Recieved deletion message: " + msg);
                    
                    using var scope = _serviceProvider.CreateScope();
                    var processor = (IEventProcessor)scope.ServiceProvider.GetRequiredService(processorType);
                    
                    await processor.ProcessAsync(msg, stoppingToken);

                    await _channel.BasicAckAsync(
                        @event.DeliveryTag,
                        multiple: false,
                        cancellationToken: stoppingToken
                    );
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing deletion message");
                    await _channel.BasicNackAsync(
                        @event.DeliveryTag,
                        multiple: false,
                        requeue: true,
                        cancellationToken: stoppingToken
                    );
                }
            };
            await _channel.BasicConsumeAsync(
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
            if (_channel != null)
            {
                await _channel.CloseAsync();
                _channel.Dispose();
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
