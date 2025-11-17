using AutoMapper;
using Gateway.API.DTOs.ParticipantionDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Participation;

public class GetUserParticipationsReplyConverter : ITypeConverter<GetUserParticipationsReply, List<GatewayParticipationResponse>>
{
    public List<GatewayParticipationResponse> Convert(GetUserParticipationsReply src, List<GatewayParticipationResponse> dest, ResolutionContext context)
    {
        if (src.ResultCase == GetUserParticipationsReply.ResultOneofCase.Participations)
        {
            return src.Participations.Items
                .Select(p => new GatewayParticipationResponse
                {
                    Id = Guid.Parse(p.Id),
                    UserId = Guid.Parse(p.UserId),
                    EventId = Guid.Parse(p.EventId),
                    IsConfirmed = p.IsConfirmed
                })
                .ToList();
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
