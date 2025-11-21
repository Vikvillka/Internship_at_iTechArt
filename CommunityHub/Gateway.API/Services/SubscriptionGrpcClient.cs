using Gateway.API.Interfaces;
using UserService.GRpc;

namespace Gateway.API.Services;

public class SubscriptionGrpcClient : ISubscriptionGrpcClient
{
    private readonly SubscriptionService.SubscriptionServiceClient _client;

    public SubscriptionGrpcClient(SubscriptionService.SubscriptionServiceClient client)
    {
        _client = client;
    }

    public async Task<CreateSubscriptionReply> SubscribeAsync(CreateSubscriptionRequest request)
    {
        return await _client.SubscribeAsync(request);
    }

    public async Task<UnsubscribeReply> UnsubscribeAsync(CreateSubscriptionRequest request)
    {
        return await _client.UnsubscribeAsync(request);
    }

    public async Task<GetUserSubscriptionsReply> GetSubscriptionsByUserAsync(GetUserSubscriptionsRequest request)
    {
        return await _client.GetSubscriptionsByUserAsync(request);
    }
}
