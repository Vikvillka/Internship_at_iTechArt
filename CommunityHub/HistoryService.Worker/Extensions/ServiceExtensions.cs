using Microsoft.EntityFrameworkCore;

using HistoryService.Infrastructure.Data;

namespace HistoryService.Worker.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<HistoryServiceDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
    }
}
