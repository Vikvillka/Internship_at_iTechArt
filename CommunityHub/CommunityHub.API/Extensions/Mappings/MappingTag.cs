using CommunityHub.API.DTOs.TagDTOs;
using CommunityHub.API.Models;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingTag
{
    public static TagResponse FromModel(this EventTag tag)
    {
        return new TagResponse
        {
            Id = tag.Id,
            Name = tag.Name
        };
    }
}

