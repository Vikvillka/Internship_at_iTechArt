using Gateway.API.Extensions.Interceptors;
using Gateway.API.Interfaces;
using Gateway.API.Interfaces.Cache;
using Gateway.API.Services;
using Gateway.API.Services.Сache;
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

        services.AddScoped<IRedisCacheApiService, RedisCacheApiService>();

        services.AddSingleton<BasicAuthClientInterceptor>();


        services.AddScoped<IAuthGrpcClient, AuthGrpcClient>();
        services.AddScoped<IUserGrpcClient, UserGrpcClient>();
        services.AddScoped<IParticipationGrpcClient, ParticipationGrpcClient>();
        services.AddScoped<ISubscriptionGrpcClient, SubscriptionGrpcClient>();

        return services;

        void AddClient<TClient>() where TClient : class
        {
            services.AddGrpcClient<TClient>(o =>
            {
                o.Address = new Uri(userServiceAddress);
            })
            .AddInterceptor<BasicAuthClientInterceptor>();
        }
    }
}
