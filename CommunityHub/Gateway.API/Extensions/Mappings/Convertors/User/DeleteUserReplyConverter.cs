using AutoMapper;

using UserService.GRpc;

namespace Gateway.API.Extensions.Mappings.Convertors.User;

public class DeleteUserReplyConverter : ITypeConverter<DeleteUserReply, bool>
{
    public bool Convert(DeleteUserReply src, bool dest, ResolutionContext context)
    {
        if (src.ResultCase == DeleteUserReply.ResultOneofCase.Success)
        {
            return src.Success;
        }

        throw new GrpcProblemDetailsException(src.Problem);
    }
}
