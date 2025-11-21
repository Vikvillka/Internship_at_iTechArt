namespace Gateway.API.DTOs.SubscriptionDTOs;

public class GatewayCreateSubscriptionRequest
{
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
}
