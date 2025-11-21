using HistoryService.Contracts.DeleteEntityDTOs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

using UserService.Application.Intarfaces.Repositories;

namespace UserService.Infrastructure.RabbitMQ;

public class RabbitMqDeletionListener : BackgroundService
{
    private readonly ConnectionFactory _factory;
    private IConnection _connection;
    private IChannel _channel;
    private readonly ILogger<RabbitMqDeletionListener> _logger;
    private readonly ICommunitySubscriptionRepository _repository;

    public RabbitMqDeletionListener(ConnectionFactory factory,
        IConnection connection,
        IChannel channel,
        ILogger<RabbitMqDeletionListener> logger,
        ICommunitySubscriptionRepository repository)
    {
        _logger = logger;
        _factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "user",
            Password = "user"
        };
        _repository = repository;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
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
        var subscriptions = await _repository.GetByCommunityIdAsync(communityId);

        if (subscriptions.Any())
        {
            await _repository.RemoveRangeAsync(subscriptions);
            _logger.LogInformation($"Deleted {subscriptions.Count} subscriptions for community {communityId}");
        }
    }
}
