using AutoMapper;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using Gateway.API.DTOs.CommunitiesDTOs;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayCommunityProfile : Profile
{
    public GatewayCommunityProfile()
    {
        CreateMap<CommunityResponse, GatewayCommunityResponse>();
        CreateMap<GatewayCreateCommunityRequest, CreateCommunityRequest>().ReverseMap();
        CreateMap<GatewayUpdateCommunityRequest, UpdateCommunityRequest>().ReverseMap();
        CreateMap<List<CommunityResponse>, List<GatewayCommunityResponse>>();
    }
}