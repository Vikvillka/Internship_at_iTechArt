using AutoMapper;

using CommunityHub.Contracts.DTOs.AuthDTOs;
using Gateway.API.DTOs.AuthDTOs;


namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayAuthProfile : Profile
{
    public GatewayAuthProfile()
    {
        CreateMap<TokenResponse, GatewayTokenResponse>();
        CreateMap<GatewayAuthRequest, AuthRequest>();
        CreateMap<GatewayRefreshRequest, RefreshRequest>();
    }
}
