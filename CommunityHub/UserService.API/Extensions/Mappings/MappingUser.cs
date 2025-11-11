using UserService.Contracts.DTOs.Enums;
using UserService.Contracts.DTOs.UserDTOs;
using UserService.Domain.Entities;

namespace UserService.API.Extensions.Mappings;

public static class MappingUser
{
    public static UserResponse FromEntity(this User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Gender = user.Gender.ToString(),
            City = user.City,
            Country = user.Country
        };
    }
}
