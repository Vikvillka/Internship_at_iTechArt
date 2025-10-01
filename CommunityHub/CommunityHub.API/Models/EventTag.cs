namespace CommunityHub.API.Models;

public class EventTag : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Event> Events { get; set; } = [];
}
