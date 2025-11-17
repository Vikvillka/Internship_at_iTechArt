using UserService.GRpc;

namespace Gateway.API.Extensions;

public static class GrpcClientsExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration config)
    {
        var userServiceAddress = config["UserServiceApi:ApiBaseUrl"];

        if (string.IsNullOrEmpty(userServiceAddress))
            throw new InvalidOperationException("ApiBaseUrl is not configured");

        services.AddGrpcClient<AuthService.AuthServiceClient>(o =>
        {
            o.Address = new Uri(userServiceAddress);
        });

        return services;
    }
}
