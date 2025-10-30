using AutoMapper;

using CommunityHub.Contracts.DTOs.UserDTOs;
using Gateway.API.DTOs.UserDTOs;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayUserProfile : Profile
{
    public GatewayUserProfile()
    {
        CreateMap<GatewayCreateUserRequest, CreateUserRequest>();
        CreateMap<UserResponse, GatewayUserResponse>();
    }
}
