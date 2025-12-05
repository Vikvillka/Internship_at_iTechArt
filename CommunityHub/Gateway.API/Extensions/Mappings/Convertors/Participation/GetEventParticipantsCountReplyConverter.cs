using AutoMapper;
using Gateway.API.DTOs.ParticipantionDTOs;
using Gateway.API.Extensions.Mappings.Convertors;
using UserService.GRpc;

public class GetEventParticipantsCountReplyConverter: ITypeConverter<GetEventParticipantsCountReply, GatewayEventParticipantsCountResponse>
{
    public GatewayEventParticipantsCountResponse Convert( GetEventParticipantsCountReply src, GatewayEventParticipantsCountResponse dest, ResolutionContext context)
    {
        if (src.ResultCase == GetEventParticipantsCountReply.ResultOneofCase.Count)
        {
            return new GatewayEventParticipantsCountResponse
            {
                Count = src.Count
            };
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
