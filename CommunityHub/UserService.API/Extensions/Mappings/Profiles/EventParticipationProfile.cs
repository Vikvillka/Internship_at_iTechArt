using AutoMapper;
using UserService.Contracts.DTOs.ParticipationDTOs;
using UserService.Domain.Entities;

namespace UserService.API.Extensions.Mappings.Profiles;

public class EventParticipationProfile : Profile
{
    public EventParticipationProfile()
    {
        CreateMap<CreateEventParticipationRequest, EventParticipation>();
    }
}