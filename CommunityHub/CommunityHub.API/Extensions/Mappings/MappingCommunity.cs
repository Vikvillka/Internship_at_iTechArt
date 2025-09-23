using CommunityHub.API.DTOs;
using CommunityHub.API.Models;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingCommunity
{
    public static CommunityResponse FromModel(this Community community)
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