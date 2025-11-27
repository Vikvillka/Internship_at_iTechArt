using AutoMapper;
using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Participation;

public class CancelParticipationReplyConverter : ITypeConverter<CancelParticipationReply, bool>
{
    public bool Convert(CancelParticipationReply src, bool dest, ResolutionContext context)
    {
        if (src.ResultCase == CancelParticipationReply.ResultOneofCase.Success)
        {
            return src.Success;
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
