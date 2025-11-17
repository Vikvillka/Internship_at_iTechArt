using Grpc.Core;
using Grpc.Core.Interceptors;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace UserService.GRpc.Interceptors;

public class BasicAuthInterceptor : Interceptor
{
    private readonly IConfiguration _config;
    private readonly ILogger<BasicAuthInterceptor> _logger;

    public BasicAuthInterceptor(IConfiguration config, ILogger<BasicAuthInterceptor> logger)
    {
        _config = config;
        _logger = logger;
    }

    private bool ValidateCredentials(string authHeader)
    {
        if (string.IsNullOrEmpty(authHeader))
            return false;

        if (!authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
            return false;

        var token = authHeader.Substring("Basic ".Length).Trim();
        string decoded;
        try
        {
            decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        }
        catch
        {
            return false;
        }

        var parts = decoded.Split(':', 2);
        if (parts.Length != 2)
            return false;

        var username = parts[0];
        var password = parts[1];

        var configUsername = _config["ApiCredentials:Username"];
        var configPassword = _config["ApiCredentials:Password"];

        return username == configUsername && password == configPassword;
    }

    public override async Task<TResponse> UnaryServerHandler<TRequest, TResponse>(
        TRequest request,
        ServerCallContext context,
        UnaryServerMethod<TRequest, TResponse> continuation)
    {
        var authHeader = context.RequestHeaders.FirstOrDefault(h => h.Key == "authorization")?.Value;

        if (!ValidateCredentials(authHeader))
        {
            _logger.LogWarning("Error in gRPC request to {Method}", context.Method);
            throw new RpcException(new Status(StatusCode.Unauthenticated, "Invalid or missing credentials"));
        }

        _logger.LogInformation("gRPC request to {Method} authenticated successfully", context.Method);
        return await continuation(request, context);
    }
}
