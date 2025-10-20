using CommunityHub.Contracts.DTOs.UserDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingUser
{
    public static UserResponse FromEntity(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
        };
    }
}
