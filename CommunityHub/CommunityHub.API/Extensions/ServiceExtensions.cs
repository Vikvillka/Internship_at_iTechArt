using Microsoft.EntityFrameworkCore;
using System.Reflection;

using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Infrastructure.Repositories;
using CommunityHub.Infrastructure.Data;

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

