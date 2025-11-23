using HistoryService.Contracts.DeleteEntityDTOs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using UserService.Application.Intarfaces.Repositories;

namespace UserService.Infrastructure.RabbitMQ;

public class RabbitMqDeletionListener : BackgroundService
{
    private readonly RabbitMqSettings _settings;
    private IConnection _connection;
    private IChannel _channel;
    private readonly ILogger<RabbitMqDeletionListener> _logger;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqDeletionListener(
        IOptions<RabbitMqSettings> options,
        ILogger<RabbitMqDeletionListener> logger,
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
        _channel = await _connection.CreateChannelAsync(
            cancellationToken: stoppingToken
        );
        
        var consumer = new AsyncEventingBasicConsumer(_channel);
        
        consumer.ReceivedAsync += async (_, @event) =>
        {
            try
            {
                var msg = Encoding.UTF8.GetString(@event.Body.ToArray());

                _logger.LogInformation("Recieved deletion message: " + msg);

                var deletionMessage = System.Text.Json.JsonSerializer.Deserialize<DeleteEntityDTO>(msg);

                if (deletionMessage != null && deletionMessage.EntityType == "Community")
                {
                    await DeleteCommunityReferences(deletionMessage.EntityId);
                }

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
            queue: "OnMyEntityDeleted",
            autoAck: false,
            consumer: consumer,
            cancellationToken: stoppingToken
        );
    }

    private async Task DeleteCommunityReferences(Guid communityId)
    {
        using var scope = _serviceProvider.CreateScope();
        var repository = scope.ServiceProvider.GetRequiredService<ICommunitySubscriptionRepository>();

        var subscriptions = await repository.GetByCommunityIdAsync(communityId);

        if (subscriptions.Any())
        {
            await repository.RemoveRangeAsync(subscriptions);
            _logger.LogInformation($"Deleted {subscriptions.Count} subscriptions for community {communityId}");
        }
    }
}
