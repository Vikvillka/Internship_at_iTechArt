using AutoMapper;
using Grpc.Core;
using UserService.Application.Intarfaces.Services;

namespace UserService.GRpc.Server.Services;

public class SubscriptionServiceImpl : SubscriptionService.SubscriptionServiceBase
{
    private readonly ICommunitySubscriptionService _service;
    private readonly IMapper _mapper;

    public SubscriptionServiceImpl(ICommunitySubscriptionService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    public override async Task<CreateSubscriptionReply> Subscribe(CreateSubscriptionRequest request, ServerCallContext context)
    {
        var subscription = await _service.SubscribeAsync(
            Guid.Parse(request.UserId),
            Guid.Parse(request.CommunityId)
        );

        return new CreateSubscriptionReply
        {
            Subscription = _mapper.Map<SubscriptionModel>(subscription)
        };
    }

    public override async Task<UnsubscribeReply> Unsubscribe(CreateSubscriptionRequest request, ServerCallContext context)
    {
        await _service.UnsubscribeAsync(
            Guid.Parse(request.UserId),
            Guid.Parse(request.CommunityId)
        );

        return new UnsubscribeReply
        {
            Success = true
        };
    }

    public override async Task<GetUserSubscriptionsReply> GetSubscriptionsByUser(GetUserSubscriptionsRequest request, ServerCallContext context)
    {
        var subscriptions = await _service.GetSubscriptionsByUserAsync(Guid.Parse(request.UserId));

        var list = new SubscriptionsList();
        list.Items.AddRange(subscriptions.Select(s => _mapper.Map<SubscriptionModel>(s)));

        return new GetUserSubscriptionsReply
        {
            Subscriptions = list
        };
    }
}
