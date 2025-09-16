namespace CommunityHub.API.Models;

public class Event : IEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public EventStatus Status { get; set; }
}

public enum EventStatus
{
    Planned,
    Comleted
}

