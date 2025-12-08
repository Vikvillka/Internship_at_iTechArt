using AutoMapper;
using Gateway.API.DTOs.ParticipantionDTOs;
using Gateway.API.Extensions.Mappings.Convertors;
using UserService.GRpc;

public class GetEventParticipantsCountReplyConverter: ITypeConverter<GetEventParticipantsCountReply, List<GatewayEventParticipantsCountResponse>>
{
    public List<GatewayEventParticipantsCountResponse> Convert(GetEventParticipantsCountReply src, List<GatewayEventParticipantsCountResponse> dest, ResolutionContext context)
    {
        if (src.ResultCase == GetEventParticipantsCountReply.ResultOneofCase.Count)
        {
            return src.Count.Items
                 .Select(i => new GatewayEventParticipantsCountResponse
                 {
                     EventId = Guid.Parse(i.EventId),
                     Count = i.Count
                 })
                 .ToList();
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
