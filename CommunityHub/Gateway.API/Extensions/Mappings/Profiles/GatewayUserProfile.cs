using AutoMapper;

using Gateway.API.DTOs.UserDTOs;
using Gateway.API.Extensions.Mappings.Convertors.User;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayUserProfile : Profile
{
    public GatewayUserProfile()
    {
        CreateMap<GatewayCreateUserRequest, RegisterUserRequest>();
        CreateMap<RegisterUserReply, GatewayUserResponse>().ConvertUsing<RegisterUserReplyConverter>();
        CreateMap<GetUserReply, GatewayUserResponse>().ConvertUsing<GetUserReplyConverter>();
        CreateMap<GetUsersReply, List<GatewayUserResponse>>().ConvertUsing<GetUsersReplyConverter>();
        CreateMap<DeleteUserReply, bool>().ConvertUsing<DeleteUserReplyConverter>();
    }
}
