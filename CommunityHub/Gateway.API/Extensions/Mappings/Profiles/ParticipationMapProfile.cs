using AutoMapper;

using Gateway.API.DTOs.ParticipantionDTOs;
using Gateway.API.Extensions.Mappings.Convertors.Participation;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class ParticipationMapProfile : Profile
{
    public ParticipationMapProfile()
    {
        CreateMap<GatewayCreateParticipationRequest, CreateParticipationRequest>()
            .ForMember(d => d.UserId, o => o.MapFrom(s => s.UserId.ToString()))
            .ForMember(d => d.EventId, o => o.MapFrom(s => s.EventId.ToString()));
        CreateMap<CreateParticipationReply, GatewayParticipationResponse>().ConvertUsing<CreateParticipationReplyConverter>();
        CreateMap<GetUserParticipationsReply, List<GatewayParticipationResponse>>().ConvertUsing<GetUserParticipationsReplyConverter>();
        CreateMap<CancelParticipationReply, bool>().ConvertUsing<CancelParticipationReplyConverter>();
    }
}
