using AutoMapper;
using Gateway.API.DTOs.SubscriptionDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Subscription;

public class GetCommunitySubscriptionsCountReplyConverter : ITypeConverter<GetCommunitySubscriptionsCountReply, GatewayCommunitySubscriptionsCountResponse>
{
    public GatewayCommunitySubscriptionsCountResponse Convert(GetCommunitySubscriptionsCountReply src, GatewayCommunitySubscriptionsCountResponse dest, ResolutionContext context)
    {
        if (src.ResultCase == GetCommunitySubscriptionsCountReply.ResultOneofCase.Count)
        {
            return new GatewayCommunitySubscriptionsCountResponse
            {
                Count = src.Count
            };
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}

