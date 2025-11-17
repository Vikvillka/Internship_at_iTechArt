using AutoMapper;

using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.Subscription;

public class UnsubscribeReplyConverter : ITypeConverter<UnsubscribeReply, bool>
{
    public bool Convert(UnsubscribeReply src, bool dest, ResolutionContext context)
    {
        if (src.ResultCase == UnsubscribeReply.ResultOneofCase.Success)
            return src.Success;

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
