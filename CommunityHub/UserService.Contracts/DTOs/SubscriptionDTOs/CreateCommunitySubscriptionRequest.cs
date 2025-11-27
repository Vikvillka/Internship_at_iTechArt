namespace UserService.Contracts.DTOs.SubscriptionDTOs;

public class CreateCommunitySubscriptionRequest
{
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
}