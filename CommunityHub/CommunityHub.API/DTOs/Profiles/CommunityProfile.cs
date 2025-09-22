using AutoMapper;
using CommunityHub.API.Models;

namespace CommunityHub.API.DTOs.Profiles;

public class CommunityProfile : Profile
{
    public CommunityProfile()
    {
        CreateMap<Community, CommunityResponse>();
        CreateMap<Event, EventResponse>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
        CreateMap<CreateCommunityRequest, Community>();
    }
}