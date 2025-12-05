namespace HistoryService.Domain.Entities;

public class HistoryRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string HistoryEventType { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Payload { get; set; } = string.Empty;
    public string TriggeredBy { get; set; } = string.Empty;
}
