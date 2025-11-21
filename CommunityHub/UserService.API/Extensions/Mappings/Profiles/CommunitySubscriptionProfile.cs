using AutoMapper;
using UserService.Contracts.DTOs.SubscriptionDTOs;
using UserService.Domain.Entities;

namespace UserService.API.Extensions.Mappings.Profiles;

public class CommunitySubscriptionProfile : Profile
{
    public CommunitySubscriptionProfile()
    {
        CreateMap<CreateCommunitySubscriptionRequest, CommunitySubscription>();
    }
}
