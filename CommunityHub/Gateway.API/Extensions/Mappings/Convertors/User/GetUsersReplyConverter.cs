using AutoMapper;
using Gateway.API.DTOs.UserDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.User;

public class GetUsersReplyConverter : ITypeConverter<GetUsersReply, List<GatewayUserResponse>>
{
    public List<GatewayUserResponse> Convert(GetUsersReply src, List<GatewayUserResponse> dest, ResolutionContext context)
    {
        if (src.ResultCase == GetUsersReply.ResultOneofCase.UsersList)
        {
            return src.UsersList.Users.Select(u => new GatewayUserResponse
            {
                Id = Guid.Parse(u.Id),
                Username = u.Username,
                Email = u.Email,
                Gender = u.Gender.ToString(),
                City = u.City,
                Country = u.Country
            }).ToList();
        }

        throw new Exception(src.Problem.Detail);
    }
}
