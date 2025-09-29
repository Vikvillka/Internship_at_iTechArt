using Microsoft.EntityFrameworkCore;
using System.Reflection;

using CommunityHub.API.Data;
using CommunityHub.API.Repositories;
using CommunityHub.API.Repositories.Interfaces;

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

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

    }
}

