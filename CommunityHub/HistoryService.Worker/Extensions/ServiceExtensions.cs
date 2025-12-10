using Microsoft.EntityFrameworkCore;

using HistoryService.Application.Intefaces.RabbitMQ;
using HistoryService.Application.Intefaces.Repositories;
using HistoryService.Application.Services.RabbitMQ;
using HistoryService.Infrastructure.Data;
using HistoryService.Infrastructure.RabbitMQ;
using HistoryService.Infrastructure.Repositories;

namespace HistoryService.Worker.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<HistoryServiceDbContext>(options =>
            options.UseNpgsql(
                config.GetConnectionString("HistoryService")
                ?? config.GetConnectionString("DefaultConnection")
            ));
        services.AddScoped<IHistoryRecordRepository, HistoryRecordRepository>();

        services.Configure<RabbitMqSettings>(config.GetSection("RabbitMq"));
        services.AddKeyedScoped<IEventProcessor, HistoryRecordProcessor>("HistoryQueue");
        services.AddHostedService<RabbitMqListener>();
    }
}
