namespace HistoryService.Infrastructure.RabbitMQ;

public class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string HistoryQueue { get; set; } = string.Empty;
}
