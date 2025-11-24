using HistoryService.Contracts.DeleteEntityDTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using UserService.Application.Intarfaces.RabbitMQ;
using UserService.Application.Intarfaces.Repositories;

namespace UserService.Infrastructure.RabbitMQ;

public class DeletionProcessor : IEventProcessor
{
    private readonly ICommunitySubscriptionRepository _repository;
    private readonly ILogger<DeletionProcessor> _logger;
    private readonly string _queueName;

    public DeletionProcessor(
        ICommunitySubscriptionRepository repository,
        ILogger<DeletionProcessor> logger,
        IOptions<RabbitMqSettings> options)
    {
        _repository = repository;
        _logger = logger;
        _queueName = options.Value.Queues["DeleteEntityQueue"];
    }
    public string QueueName => _queueName;

    public async Task ProcessAsync(string message, CancellationToken cancellationToken)
    {
        var dto = System.Text.Json.JsonSerializer.Deserialize<DeleteEntityDTO>(message);
        if (dto != null && dto.EntityType == "Community")
        {
            var subscriptions = await _repository.GetByCommunityIdAsync(dto.EntityId);
            if (subscriptions.Any())
            {
                await _repository.RemoveRangeAsync(subscriptions);
                _logger.LogInformation($"Deleted {subscriptions.Count} subscriptions for community {dto.EntityId}");
            }
        }
    }
}
