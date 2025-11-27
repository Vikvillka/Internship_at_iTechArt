namespace Gateway.API.DTOs.SubscriptionDTOs;

public class GatewaySubscriptionResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
    public string CommunityName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
