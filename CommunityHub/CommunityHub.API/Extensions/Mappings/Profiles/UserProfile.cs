using AutoMapper;
using CommunityHub.API.DTOs.UserDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Extensions.Mappings.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponse>();
        CreateMap<CreateUserRequest, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}
