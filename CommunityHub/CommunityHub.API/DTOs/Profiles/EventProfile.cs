using AutoMapper;
using CommunityHub.API.DTOs.EventDTOs;
using CommunityHub.API.Models;

namespace CommunityHub.API.DTOs.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        
        CreateMap<CreateEventRequest, Event>()
            .ForMember(dest => dest.CommunityId, opt => opt.Ignore()) 
            .ForMember(dest => dest.Community, opt => opt.Ignore());

        CreateMap<UpdateEventRequest, Event>();
    }
}