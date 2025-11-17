using AutoMapper;

using Gateway.API.DTOs.AuthDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Auth;

public class GetTokensReplyConverter : ITypeConverter<GetTokensReply, GatewayTokenResponse>
{
    public GatewayTokenResponse Convert(GetTokensReply src, GatewayTokenResponse dest, ResolutionContext context)
    {
        if (src.ResultCase == GetTokensReply.ResultOneofCase.Tokens)
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
