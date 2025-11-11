using Microsoft.EntityFrameworkCore;

using UserService.Infrastructure.Data;

namespace UserService.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
    }
}

