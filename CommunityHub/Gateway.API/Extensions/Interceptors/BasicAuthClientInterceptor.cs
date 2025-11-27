using Grpc.Core;
using Grpc.Core.Interceptors;
using System.Text;

namespace Gateway.API.Extensions.Interceptors;

public class BasicAuthClientInterceptor : Interceptor
{
    private readonly string _username;
    private readonly string _password;

    public BasicAuthClientInterceptor(IConfiguration config)
    {
        _username = config["UserServiceApi:ApiCredentials:Username"] ?? throw new InvalidOperationException("Username not configured");
        _password = config["UserServiceApi:ApiCredentials:Password"] ?? throw new InvalidOperationException("Password not configured");
    }

    private string GetBasicAuthHeader()
    {
        var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_username}:{_password}"));
        return $"Basic {token}";
    }

    public override AsyncUnaryCall<TResponse> AsyncUnaryCall<TRequest, TResponse>(
        TRequest request,
        ClientInterceptorContext<TRequest, TResponse> context,
        AsyncUnaryCallContinuation<TRequest, TResponse> continuation)
    {
        var headers = new Metadata
        {
            { "Authorization", GetBasicAuthHeader() }
        };

        var newOptions = context.Options.WithHeaders(headers);
        var newContext = new ClientInterceptorContext<TRequest, TResponse>(context.Method, context.Host, newOptions);

        return continuation(request, newContext);
    }
}
