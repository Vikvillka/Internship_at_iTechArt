using AutoMapper;

using Gateway.API.DTOs.SubscriptionDTOs;
using Gateway.API.Extensions.Mappings.Convertors.Subscription;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewaySubscriptionProfile : Profile
{
    public GatewaySubscriptionProfile()
    {
        CreateMap<GatewayCreateSubscriptionRequest, CreateSubscriptionRequest>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.CommunityId, opt => opt.MapFrom(src => src.CommunityId.ToString()));
        CreateMap<CreateSubscriptionReply, GatewaySubscriptionResponse>().ConvertUsing<CreateSubscriptionReplyConverter>();
        CreateMap<GetUserSubscriptionsReply, List<GatewaySubscriptionResponse>>().ConvertUsing<GetUserSubscriptionsReplyConverter>();
        CreateMap<UnsubscribeReply, bool>().ConvertUsing<UnsubscribeReplyConverter>();
        CreateMap<GetCommunitySubscriptionsCountReply, List<GatewayCommunitySubscriptionsCountResponse>>().ConvertUsing<GetCommunitySubscriptionsCountReplyConverter>();
    }
}
