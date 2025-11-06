namespace Gateway.API.DTOs.EventDTOs;

public class GatewayUpdateEventRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int Duration { get; set; }             
    public IFormFile? Image { get; set; }         
    public string? OldImagePath { get; set; }
    public Guid CommunityId { get; set; }
}

