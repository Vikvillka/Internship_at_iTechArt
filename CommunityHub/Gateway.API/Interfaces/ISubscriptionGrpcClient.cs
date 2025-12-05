using UserService.GRpc;

namespace Gateway.API.Interfaces;

public interface ISubscriptionGrpcClient
{
    Task<CreateSubscriptionReply> SubscribeAsync(CreateSubscriptionRequest request);
    Task<UnsubscribeReply> UnsubscribeAsync(CreateSubscriptionRequest request);
    Task<GetUserSubscriptionsReply> GetSubscriptionsByUserAsync(GetUserSubscriptionsRequest request);
    Task<GetCommunitySubscriptionsCountReply> GetCommunitySubscriptionsCountAsync(GetCommunitySubscriptionsCountRequest request);
}
