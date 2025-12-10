using AutoMapper;

using CommunityHub.Contracts.DTOs.EventDTOs;
using Gateway.API.DTOs.Common;
using Gateway.API.DTOs.EventDTOs;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayEventProfile : Profile
{
    public GatewayEventProfile()
    {
        CreateMap<EventResponse, GatewayEventResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<GatewayCreateEventRequest, CreateEventRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
        CreateMap<GatewayUpdateEventRequest, UpdateEventRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
        CreateMap<GatewayEventSearchRequest, EventSearchRequest>();
        CreateMap<CommunityHub.Contracts.DTOs.Common.PagedResponse<EventResponse>, PagedResponse<GatewayEventResponse>>();
    }
}