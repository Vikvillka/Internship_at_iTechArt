using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using UserService.Application.Intarfaces.Repositories;
using UserService.Application.Intarfaces.Services;
using UserService.Application.Services;
using UserService.GRpc.Interceptors;
using UserService.Infrastructure.Data;
using UserService.Infrastructure.RabbitMQ;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Services;

namespace UserService.GRpc.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserServiceDbContext>(options =>
        options.UseNpgsql(
                config.GetConnectionString("UserService")
                ?? config.GetConnectionString("DefaultConnection")
            ));

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddGrpc(options =>
        {
            options.Interceptors.Add<GrpcExceptionInterceptor>();
            options.Interceptors.Add<BasicAuthInterceptor>();
        });

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommunitySubscriptionRepository, CommunitySubscriptionRepository>();
        services.AddScoped<IEventParticipationRepository, EventParticipationRepository>();

        services.AddScoped<IUserService, Application.Services.UserService>();
        services.AddScoped<IEventParticipationService, EventParticipationService>();
        services.AddScoped<ICommunitySubscriptionService, CommunitySubscriptionService>();
        services.AddScoped<IJwtService, JwtService>();

        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));
        services.AddHostedService<RabbitMqDeletionListener>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}

