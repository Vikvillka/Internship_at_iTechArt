using AutoMapper;

using CommunityHub.Contracts.DTOs.EventDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Extensions.Mappings.Profiles;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateEventRequest, Event>();
        CreateMap<UpdateEventRequest, Event>();
    }
}