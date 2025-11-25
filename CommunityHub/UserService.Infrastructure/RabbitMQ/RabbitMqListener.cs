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
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly RabbitMqSettings _settings;
    private readonly ILogger<RabbitMqListener> _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _connectionString;

    public RabbitMqListener(
        ILogger<RabbitMqListener> logger,
        IConfiguration config,
        IServiceProvider serviceProvider,
        IOptions<RabbitMqSettings> settings)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _connectionString = config.GetConnectionString("messaging")!;
        _settings = settings.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var _factory = new ConnectionFactory()
        {
            Uri = new Uri(_connectionString)
        };

        _connection = await _factory.CreateConnectionAsync(stoppingToken);
       
        using var scope = _serviceProvider.CreateScope();
        var processors = scope.ServiceProvider.GetServices<IEventProcessor>();

        foreach (var processor in processors)
        {
            var queueName = processor.QueueName;
            var exchangeName = _settings.DeleteEntityExchange;

            _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
            
            await _channel.ExchangeDeclareAsync(
                exchange: exchangeName,
                type: ExchangeType.Fanout,
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
               routingKey: ""
            );

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.ReceivedAsync += async (_, @event) =>
            {
                try
                {
                    using var innerScope = _serviceProvider.CreateScope();
                    var proc = innerScope.ServiceProvider
                        .GetServices<IEventProcessor>()
                        .First(p => p.QueueName == queueName);

                    var msg = Encoding.UTF8.GetString(@event.Body.ToArray());

                    _logger.LogInformation("Recieved deletion message: " + msg);

                    await proc.ProcessAsync(msg, stoppingToken);

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
}
