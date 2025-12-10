namespace CommunityHub.Contracts.DTOs.Common;

public class PagedResponse<T>
{
    public IList<T> Items { get; set; } = [];
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
