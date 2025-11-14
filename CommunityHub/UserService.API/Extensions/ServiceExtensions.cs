using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

using UserService.Infrastructure.Data;
using UserService.API.ExceptionHandlers;
using UserService.Application.Intarfaces.Repositories;
using UserService.Application.Intarfaces.Services;
using UserService.Infrastructure.Repositories;
using UserService.Infrastructure.Services;
using UserService.Application.Services;

namespace UserService.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICommunitySubscriptionRepository, CommunitySubscriptionRepository>();
        services.AddScoped<IEventParticipationRepository, EventParticipationRepository>();
        
        services.AddScoped<IUserService, Application.Services.UserService>();
        services.AddScoped<IEventParticipationService, EventParticipationService>();
        services.AddScoped<ICommunitySubscriptionService, CommunitySubscriptionService>();
        services.AddScoped<IJwtService, JwtService>();
        
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();

        services.AddAuthenticationSchemes(config);

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();
    }
}

