namespace Gateway.API.DTOs.SubscriptionDTOs;

public class GatewayCommunitySubscriptionsCountResponse
{
    public Guid CommunityId { get; set; }
    public int Count { get; set; }
}
