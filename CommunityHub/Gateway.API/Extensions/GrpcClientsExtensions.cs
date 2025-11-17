using Gateway.API.Extensions.Interceptors;
using UserService.GRpc;

namespace Gateway.API.Extensions;

public static class GrpcClientsExtensions
{
    public static IServiceCollection AddGrpcClients(this IServiceCollection services, IConfiguration config)
    {
        var userServiceAddress = config["UserServiceApi:ApiBaseUrl"];

        if (string.IsNullOrEmpty(userServiceAddress))
            throw new InvalidOperationException("ApiBaseUrl is not configured");

        AddClient<AuthService.AuthServiceClient>();
        AddClient<UserService.GRpc.UserService.UserServiceClient>();
        AddClient<SubscriptionService.SubscriptionServiceClient>();
        AddClient<ParticipationService.ParticipationServiceClient>();

        void AddClient<TClient>() where TClient : class
        {
            services.AddGrpcClient<TClient>(o =>
            {
                o.Address = new Uri(userServiceAddress);
            })
            .AddInterceptor(sp =>
            {
                 var config = sp.GetRequiredService<IConfiguration>();
                 return new BasicAuthClientInterceptor(config);
            });
        }

        return services;
    }
}
