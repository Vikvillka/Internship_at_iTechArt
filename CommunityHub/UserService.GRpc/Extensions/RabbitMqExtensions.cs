using UserService.Application.Intarfaces.RabbitMQ;
using UserService.Infrastructure.RabbitMQ;

namespace UserService.GRpc.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMqEventProcessors(
        this IServiceCollection services, string queueKey, Type processorType)
    {
        services.AddScoped(processorType);
        services.AddScoped(typeof(IEventProcessor), sp => sp.GetRequiredService(processorType));

        services.Configure<RabbitMqListenerOptions>(opts =>
        {
            if (!opts.QueueMappings.ContainsKey(processorType))
                opts.QueueMappings[processorType] = queueKey;
        });
        return services;
    }
}
