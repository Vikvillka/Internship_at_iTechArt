using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Repositories;

public interface ICommunitySubscriptionRepository : IRepository<CommunitySubscription>
{
    Task<CommunitySubscription?> GetByUserAndCommunityAsync(Guid userId, Guid communityId);
    Task<IList<CommunitySubscription>> GetByUserAsync(Guid userId);
    Task<IList<CommunitySubscription>> GetByCommunityAsync(Guid communityId);
    Task<bool> UpdateStatusAsync(Guid id, bool isActive);
    Task<IList<CommunitySubscription>> GetByCommunityIdAsync(Guid communityId);
    Task RemoveRangeAsync(IList<CommunitySubscription> subscriptions);
}
