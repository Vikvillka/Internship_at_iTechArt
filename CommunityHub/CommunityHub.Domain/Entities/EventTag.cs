namespace CommunityHub.Domain.Entities;

public class EventTag : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public ICollection<Event> Events { get; set; } = [];
}
