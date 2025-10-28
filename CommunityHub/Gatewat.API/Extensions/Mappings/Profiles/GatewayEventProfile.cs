using AutoMapper;

using CommunityHub.Contracts.DTOs.EventDTOs;
using Gateway.API.DTOs.EventDTOs;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<EventResponse, GatewayEventResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<GatewayCreateEventRequest, CreateEventRequest>().ReverseMap();
        CreateMap<GatewayUpdateEventRequest, UpdateEventRequest>().ReverseMap();
        CreateMap<List<EventResponse>, List<GatewayEventResponse>>();
    }
}