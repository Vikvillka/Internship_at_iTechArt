namespace HistoryService.Infrastructure.RabbitMQ;

public class RabbitMqSettings
{
    public Dictionary<string, string> Queues { get; set; } = [];
    public string HistoryExchange { get; set; } = string.Empty;
}
