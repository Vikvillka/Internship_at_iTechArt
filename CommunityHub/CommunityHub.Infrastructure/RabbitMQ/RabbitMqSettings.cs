namespace CommunityHub.Infrastructure.RabbitMQ;

public class RabbitMqSettings
{
    public string DeleteEntityExchange { get; set; } = string.Empty;
    public string HistoryExchange { get; set; } = string.Empty;
    public string HistoryQueue {  get; set; } = string.Empty;
    public string DeleteEntityQueue { get; set; } = string.Empty;
}
