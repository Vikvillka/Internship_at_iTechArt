namespace Gateway.API.DTOs.CommunitiesDTOs;

public class GatewayCreateCommunityRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
}