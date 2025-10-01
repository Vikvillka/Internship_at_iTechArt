using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Application.Services;
using CommunityHub.Infrastructure.Data;
using CommunityHub.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CommunityHub.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<CommunityHubDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddScoped<ICommunityRepository, CommunityRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        services.AddScoped<ICommunityService, CommunityService>();
        services.AddScoped<IEventService, EventService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

    }
}

