namespace UserService.Infrastructure.RabbitMQ;

public class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public Dictionary<string, string> Queues { get; set; } = [];
    public string DeleteEntityExchange { get; set; } = string.Empty;
}
