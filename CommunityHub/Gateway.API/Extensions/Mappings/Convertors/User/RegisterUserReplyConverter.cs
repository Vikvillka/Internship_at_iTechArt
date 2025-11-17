using AutoMapper;

using Gateway.API.DTOs.UserDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.User;

public class RegisterUserReplyConverter : ITypeConverter<RegisterUserReply, GatewayUserResponse>
{
    public GatewayUserResponse Convert(RegisterUserReply src, GatewayUserResponse dest, ResolutionContext context)
    {
        if (src.ResultCase == RegisterUserReply.ResultOneofCase.User)
        {
            return new GatewayUserResponse
            {
                Id = Guid.Parse(src.User.Id),
                Username = src.User.Username,
                Email = src.User.Email,
                Gender = src.User.Gender.ToString(),
                City = src.User.City,
                Country = src.User.Country
            };
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
