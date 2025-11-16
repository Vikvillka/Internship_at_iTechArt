using System.Reflection;
using UserService.Application.Intarfaces.Services;
using UserService.Application.Services;
using UserService.Infrastructure.Services;

namespace UserService.GRpc.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<IUserService, Application.Services.UserService>();
        services.AddScoped<IEventParticipationService, EventParticipationService>();
        services.AddScoped<ICommunitySubscriptionService, CommunitySubscriptionService>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}

