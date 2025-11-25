namespace HistoryService.Infrastructure.RabbitMQ;

public class RabbitMqSettings
{
    public string HistoryQueue { get; set; } = string.Empty;
    public string HistoryExchange { get; set; } = string.Empty;
}
