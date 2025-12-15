namespace CommunityHub.Contracts.DTOs.CommunitiesDTOs;

public class CommunitySearchRequest
{
    public string? Keywords { get; set; }
    public string? Category { get; set; }
    public string? Location { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
