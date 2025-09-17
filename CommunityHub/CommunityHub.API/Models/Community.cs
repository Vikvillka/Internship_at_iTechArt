namespace CommunityHub.API.Models;

public class Community : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public bool IsDeleted { get; set; } = false;

    public List<Event> Events { get; set; } = [];
}