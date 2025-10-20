using CommunityHub.Contracts.DTOs.Enums;

namespace CommunityHub.Contracts.DTOs.EventDTOs;

public class CreateEventRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public EventStatus Status { get; set; } = EventStatus.Planned;
    public Guid CommunityId { get; set; }
    public List<Guid> TagIds { get; set; } = [];
}

