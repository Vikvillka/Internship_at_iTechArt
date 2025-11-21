using AutoMapper;
using Gateway.API.DTOs.ParticipantionDTOs;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Participation;

public class CreateParticipationReplyConverter : ITypeConverter<CreateParticipationReply, GatewayParticipationResponse>
{
    public GatewayParticipationResponse Convert(CreateParticipationReply src, GatewayParticipationResponse dest, ResolutionContext context)
    {
        if (src.ResultCase == CreateParticipationReply.ResultOneofCase.Participation)
        {
            return new GatewayParticipationResponse
            {
                Id = Guid.Parse(src.Participation.Id),
                UserId = Guid.Parse(src.Participation.UserId),
                EventId = Guid.Parse(src.Participation.EventId),
                IsConfirmed = src.Participation.IsConfirmed
            };
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
