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
    private readonly RabbitMqSettings _settings;
    private IConnection? _connection;
    private IChannel? _channel;
    private readonly ILogger<RabbitMqListener> _logger;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqListener(
        IOptions<RabbitMqSettings> options,
        ILogger<RabbitMqListener> logger,
        IServiceProvider serviceProvider)
    {
        _logger = logger;
        _settings = options.Value;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var _factory = new ConnectionFactory()
        {
            HostName = _settings.Host,
            UserName = _settings.Username,
            Password = _settings.Password,
        };

        _connection = await _factory.CreateConnectionAsync(stoppingToken);
       
        using var scope = _serviceProvider.CreateScope();
        var processors = scope.ServiceProvider.GetServices<IEventProcessor>();

        foreach (var processor in processors)
        {
            var queueName = processor.QueueName;
            
            _channel = await _connection.CreateChannelAsync(cancellationToken: stoppingToken);
            await _channel.QueueDeclareAsync(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false
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
