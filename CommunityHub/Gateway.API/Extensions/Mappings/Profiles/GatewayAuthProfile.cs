using AutoMapper;

using Gateway.API.DTOs.AuthDTOs;
using Gateway.API.Extensions.Mappings.Convertors.Auth;
using UserService.GRpc;


namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayAuthProfile : Profile
{
    public GatewayAuthProfile()
    {
        CreateMap<TokenModel, GatewayTokenResponse>();
        CreateMap<GatewayAuthRequest, AuthRequest>();
        CreateMap<GatewayRefreshRequest, RefreshRequest>();
        CreateMap<GetTokensReply, GatewayTokenResponse>().ConvertUsing<GetTokensReplyConverter>();
        CreateMap<RefreshReply, GatewayTokenResponse>().ConvertUsing<RefreshReplyConverter>();
    }
}
