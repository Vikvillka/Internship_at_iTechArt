using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

using CommunityHub.API.ExceptionHandlers;
using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Application.Services;
using CommunityHub.Infrastructure.Data;
using CommunityHub.Infrastructure.Repositories;
using CommunityHub.Infrastructure.Services;

namespace CommunityHub.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CommunityHubDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("CommunityHub")
                ?? config.GetConnectionString("DefaultConnection")
            ));

        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<ICommunityService, CommunityService>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<ITagService, TagService>();
        services.AddScoped<IImageService, ImageService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();

        services.AddAuthenticationSchemes(config);

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();
    }
}

