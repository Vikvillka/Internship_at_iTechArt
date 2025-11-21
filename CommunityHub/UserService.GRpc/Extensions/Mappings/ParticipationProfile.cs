using AutoMapper;
using UserService.Domain.Entities;

namespace UserService.GRpc.Extensions.Mappings;

public class ParticipationProfile : Profile
{
    public ParticipationProfile()
    {
        CreateMap<EventParticipation, ParticipationModel>();
    }
}
