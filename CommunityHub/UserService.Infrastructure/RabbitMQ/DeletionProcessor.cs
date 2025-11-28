using Microsoft.Extensions.Logging;
using System.Text.Json;

using HistoryService.Contracts.DeleteEntityDTOs;
using UserService.Application.Intarfaces.RabbitMQ;
using UserService.Application.Intarfaces.Repositories;

namespace UserService.Infrastructure.RabbitMQ;

public class DeletionProcessor : IEventProcessor
{
    private readonly ICommunitySubscriptionRepository _repository;
    private readonly ILogger<DeletionProcessor> _logger;

    public DeletionProcessor(
        ICommunitySubscriptionRepository repository,
        ILogger<DeletionProcessor> logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task ProcessAsync(string message, CancellationToken cancellationToken)
    {
        var dto = JsonSerializer.Deserialize<DeleteEntityDTO>(message);
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
