using AutoMapper;

using CommunityHub.Contracts.DTOs.EventDTOs;
using Gateway.API.DTOs.EventDTOs;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayEventProfile : Profile
{
    public GatewayEventProfile()
    {
        CreateMap<EventResponse, GatewayEventResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<GatewayCreateEventRequest, CreateEventRequest>();
        CreateMap<GatewayUpdateEventRequest, UpdateEventRequest>();
    }
}