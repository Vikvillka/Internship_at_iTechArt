using AutoMapper;
using Gateway.API.DTOs.ParticipantionDTOs;
using Gateway.API.DTOs.SubscriptionDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Subscription;

public class GetCommunitySubscriptionsCountReplyConverter : ITypeConverter<GetCommunitySubscriptionsCountReply, List<GatewayCommunitySubscriptionsCountResponse>>
{
    public List<GatewayCommunitySubscriptionsCountResponse> Convert(GetCommunitySubscriptionsCountReply src, List<GatewayCommunitySubscriptionsCountResponse> dest, ResolutionContext context)
    {
        if (src.ResultCase == GetCommunitySubscriptionsCountReply.ResultOneofCase.Count)
        {
            return src.Count.Items
                 .Select(i => new GatewayCommunitySubscriptionsCountResponse
                 {
                     CommunityId = Guid.Parse(i.CommunityId),
                     Count = i.Count
                 })
                 .ToList();
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}

