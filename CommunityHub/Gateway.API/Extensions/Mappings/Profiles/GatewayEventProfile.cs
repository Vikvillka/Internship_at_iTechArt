using AutoMapper;
using Gateway.API.DTOs.EventDTOs;

using CommunityHub.Contracts.DTOs.Enums;
using CommunityHub.Contracts.DTOs.EventDTOs;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayEventProfile : Profile
{
    public GatewayEventProfile()
    {
        CreateMap<EventResponse, GatewayEventResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<GatewayCreateEventRequest, CreateEventRequest>()
            .ForMember(dest => dest.ImagePath, opt => opt.Ignore());
        CreateMap<GatewayUpdateEventRequest, UpdateEventRequest>();
    }
}