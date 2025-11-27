using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Services;

public interface ICommunitySubscriptionService
{
    Task<CommunitySubscription> SubscribeAsync(Guid userId, Guid communityId);
    Task<CommunitySubscription> UnsubscribeAsync(Guid userId, Guid communityId);
    Task<IList<CommunitySubscription>> GetSubscriptionsByUserAsync(Guid userId);
}
