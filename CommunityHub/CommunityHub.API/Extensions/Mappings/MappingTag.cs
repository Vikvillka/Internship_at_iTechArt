using CommunityHub.Contracts.DTOs.TagDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingTag
{
    public static TagResponse FromEntity(this EventTag tag)
    {
        return new TagResponse
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }
}

