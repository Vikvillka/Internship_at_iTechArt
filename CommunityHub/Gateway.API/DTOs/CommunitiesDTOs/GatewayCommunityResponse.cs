using Gateway.API.DTOs.EventDTOs;

namespace Gateway.API.DTOs.CommunitiesDTOs;

public class GatewayCommunityResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }

    public List<GatewayEventResponse> Events { get; set; } = [];
}