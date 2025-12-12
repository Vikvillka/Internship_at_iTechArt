namespace CommunityHub.Contracts.DTOs.EventDTOs;

public class EventSearchRequest
{
    public string? Keywords { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? Category { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
