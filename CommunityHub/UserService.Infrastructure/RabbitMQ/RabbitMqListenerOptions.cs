namespace UserService.Infrastructure.RabbitMQ;

public class RabbitMqListenerOptions
{
    public Dictionary<Type, string> QueueMappings { get; set; } = [];
}
