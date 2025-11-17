using AutoMapper;
using Gateway.API.DTOs.SubscriptionDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Subscription;

public class GetUserSubscriptionsReplyConverter : ITypeConverter<GetUserSubscriptionsReply, List<GatewaySubscriptionResponse>>
{
    public List<GatewaySubscriptionResponse> Convert(GetUserSubscriptionsReply src, List<GatewaySubscriptionResponse> dest, ResolutionContext context)
    {
        if (src.ResultCase == GetUserSubscriptionsReply.ResultOneofCase.Subscriptions)
        {
            return src.Subscriptions.Items
                .Select(s => new GatewaySubscriptionResponse
                {
                    Id = Guid.Parse(s.Id),
                    UserId = Guid.Parse(s.UserId),
                    CommunityId = Guid.Parse(s.CommunityId),
                    IsActive = s.IsActive
                })
                .ToList();
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
