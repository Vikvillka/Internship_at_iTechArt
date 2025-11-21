namespace HistoryService.Contracts.HistoryRecordDTOs;

public class HistoryRecordDTO
{
    public string Type { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string Payload { get; set; } = string.Empty;
    public string TriggeredBy { get; set; } = string.Empty;
}
