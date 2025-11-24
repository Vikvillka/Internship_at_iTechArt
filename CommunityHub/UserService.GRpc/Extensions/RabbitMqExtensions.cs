using UserService.Application.Intarfaces.RabbitMQ;
using UserService.Infrastructure.RabbitMQ;

namespace UserService.GRpc.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMqEventProcessors(
        this IServiceCollection services, params Type[] procTypes)
    {
        foreach (var type in procTypes)
        {
            services.AddScoped(typeof(IEventProcessor), type);
        }
        services.AddHostedService<RabbitMqListener>();
        return services;
    }
}
