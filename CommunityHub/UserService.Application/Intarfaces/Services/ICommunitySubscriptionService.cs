using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Services;

public interface ICommunitySubscriptionService
{
    Task SubscribeAsync(Guid userId, Guid communityId);
    Task UnsubscribeAsync(Guid userId, Guid communityId);
    Task<IList<CommunitySubscription>> GetSubscriptionsByUserAsync(Guid userId);
}
