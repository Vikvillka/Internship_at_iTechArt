namespace UserService.Domain.Entities;

public class CommunitySubscription : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid CommunityId { get; set; }
    public bool IsActive { get; set; } = true;
}
