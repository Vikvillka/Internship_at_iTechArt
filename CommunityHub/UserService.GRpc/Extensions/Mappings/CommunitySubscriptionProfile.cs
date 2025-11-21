using AutoMapper;
using UserService.Domain.Entities;

namespace UserService.GRpc.Extensions.Mappings;

public class CommunitySubscriptionProfile : Profile
{
    public CommunitySubscriptionProfile()
    {
        CreateMap<CommunitySubscription, SubscriptionModel>();
    }
}
