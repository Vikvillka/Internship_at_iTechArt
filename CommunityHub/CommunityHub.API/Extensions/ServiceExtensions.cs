using CommunityHub.API.ExceptionHandlers;
using CommunityHub.Application.Interfaces.RabbitMQ;
using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Application.Services;
using CommunityHub.Infrastructure.Data;
using CommunityHub.Infrastructure.RabbitMQ;
using CommunityHub.Infrastructure.Repositories;
using CommunityHub.Infrastructure.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));
        services.AddSingleton<IRabbitMqPublisher, RabbitMqPublisher>();

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();
    }
}

