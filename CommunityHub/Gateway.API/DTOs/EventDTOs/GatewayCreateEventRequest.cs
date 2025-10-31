using Gateway.API.DTOs.Enums;

namespace Gateway.API.DTOs.EventDTOs;

public class GatewayCreateEventRequest
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public GatewayEventStatusDto Status { get; set; } = GatewayEventStatusDto.Planned;
    public Guid CommunityId { get; set; }
    public List<Guid> TagIds { get; set; } = [];
}

