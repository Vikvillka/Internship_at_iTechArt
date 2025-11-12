namespace UserService.Domain.Entities;

public class EventParticipation : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public bool IsConfirmed { get; set; } = true;
}
