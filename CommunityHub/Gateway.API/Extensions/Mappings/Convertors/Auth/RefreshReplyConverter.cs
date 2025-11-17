using AutoMapper;

using Gateway.API.DTOs.AuthDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Auth;

public class RefreshReplyConverter : ITypeConverter<RefreshReply, GatewayTokenResponse>
{
    public GatewayTokenResponse Convert(RefreshReply src, GatewayTokenResponse dest, ResolutionContext context)
    {
        if (src.ResultCase == RefreshReply.ResultOneofCase.Tokens)
        {
            return new GatewayTokenResponse
            {
                AccessToken = src.Tokens.AccessToken,
                RefreshToken = src.Tokens.RefreshToken
            };
        }

        throw new Exception(src.Problem.Detail);
    }
}
