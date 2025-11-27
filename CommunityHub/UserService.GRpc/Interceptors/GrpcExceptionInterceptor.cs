using Grpc.Core;
using Grpc.Core.Interceptors;

using UserService.Domain.Exceptions;
using UserService.GRpc.Interfaces;

namespace UserService.GRpc.Interceptors;

public class GrpcExceptionInterceptor : Interceptor
{
    private readonly ILogger<GrpcExceptionInterceptor> _logger;

    public GrpcExceptionInterceptor(ILogger<GrpcExceptionInterceptor> logger)
    {
        _logger = logger;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        try
        {
            return await continuation(request, context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled gRPC exception for {Method}", context.Method);

            var problem = new Common.ProblemDetails
            {
                Title = ex switch
                {
                    NotFoundException nf => nf.Error,
                    BadRequestException br => br.Error,
                    ConflictException cf => cf.Error,
                    UnauthorizedException ua => ua.Error,
                    _ => "InternalError"
                },
                Detail = ex.Message,
                Status = ex switch
                {
                    NotFoundException => 404,
                    BadRequestException => 400,
                    ConflictException => 409,
                    UnauthorizedException => 401,
                    _ => 500
                },
                Type = ex.GetType().Name,
                Instance = context.Method
            };

            if(typeof(IProblemReply).IsAssignableFrom(typeof(TResponse)))
            {
                var reply = (IProblemReply)Activator.CreateInstance(typeof(TResponse))!;
                reply.SetProblem(problem);
                return (TResponse)reply;
            }

            throw new RpcException(new Status(StatusCode.Internal, ex.Message));
        }
    }
}
