namespace UserService.Contracts.DTOs.SubscriptionDTOs;

public class CommunitySubscriptionResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
    public bool IsActive { get; set; }
}
