namespace CommunityHub.Domain.Entities;

public class Event : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public EventStatus Status { get; set; }

    public Guid CommunityId { get; set; }
    public Community Community { get; set; } = null!;
    public ICollection<EventTag> Tags { get; set; } = [];
}

public enum EventStatus
{
    Planned,
    Completed,
    Cancelled
}