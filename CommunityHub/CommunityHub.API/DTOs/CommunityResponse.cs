using CommunityHub.API.Models;

namespace CommunityHub.API.DTOs;

public class CommunityResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public List<EventResponse> Events { get; set; } = [];

    public static CommunityResponse FromModel(Community community)
    {
        return new CommunityResponse
        {
            Id = community.Id,
            Name = community.Name,
            Description = community.Description,
            Category = community.Category,
            City = community.City,
            Country = community.Country,
            Events = community.Events.Select(EventResponse.FromModel).ToList()
        };
    }
}