using AutoMapper;

using Gateway.API.DTOs.SubscriptionDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Subscription;

public class CreateSubscriptionReplyConverter : ITypeConverter<CreateSubscriptionReply, GatewaySubscriptionResponse>
{
    public GatewaySubscriptionResponse Convert(CreateSubscriptionReply src, GatewaySubscriptionResponse dest, ResolutionContext context)
    {
        if (src.ResultCase == CreateSubscriptionReply.ResultOneofCase.Subscription)
        {
            return new GatewaySubscriptionResponse
            {
                Id = Guid.Parse(src.Subscription.Id),
                UserId = Guid.Parse(src.Subscription.UserId),
                CommunityId = Guid.Parse(src.Subscription.CommunityId),
                IsActive = src.Subscription.IsActive
            };
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
