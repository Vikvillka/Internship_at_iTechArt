using Microsoft.EntityFrameworkCore;

using HistoryService.Infrastructure.Data;
using HistoryService.Infrastructure.RabbitMQ;

namespace HistoryService.Worker.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<HistoryServiceDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));
        services.AddHostedService<RabbitMqListener>();
    }
}
