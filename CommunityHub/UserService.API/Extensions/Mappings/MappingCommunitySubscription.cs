using UserService.Contracts.DTOs.SubscriptionDTOs;
using UserService.Domain.Entities;

namespace UserService.API.Extensions.Mappings;

public static class MappingCommunitySubscription
{
    public static CommunitySubscriptionResponse FromEntity(this CommunitySubscription subscription)
    {
        return new CommunitySubscriptionResponse
        {
            Id = subscription.Id,
            UserId = subscription.UserId,
            CommunityId = subscription.CommunityId,
            IsActive = subscription.IsActive
        };
    }
}
