using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingCommunity
{
    public static CommunityResponse FromEntity(this Community community)
    {
        return new CommunityResponse
        {
            Id = community.Id,
            Name = community.Name,
            Description = community.Description,
            Category = community.Category,
            City = community.City,
            Country = community.Country,
            Events = community.Events.Select(e => e.FromEntity()).ToList()
        };
    }
}