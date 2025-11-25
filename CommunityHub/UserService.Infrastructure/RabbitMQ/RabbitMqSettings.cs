namespace UserService.Infrastructure.RabbitMQ;

public class RabbitMqSettings
{
    public Dictionary<string, string> Queues { get; set; } = [];
    public string DeleteEntityExchange { get; set; } = string.Empty;
}
