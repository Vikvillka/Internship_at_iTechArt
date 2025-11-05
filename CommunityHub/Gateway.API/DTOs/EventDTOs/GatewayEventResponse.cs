using Gateway.API.DTOs.TagDTOs;

namespace Gateway.API.DTOs.EventDTOs;

public class GatewayEventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int Duration { get; set; } 
    public string? ImagePath { get; set; }
    public List<GatewayTagResponse> Tags { get; set; } = [];
}