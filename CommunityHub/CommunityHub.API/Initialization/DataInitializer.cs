using CommunityHub.API.Models;

namespace CommunityHub.API.Initialization;

public static class DataInitializer
{
    public static void Seed()
    {
        if (DataStorage.groups.Any()) return;

        var defaultEvent = new Event
        {
            Title = "Short Story Discussion: Harrison Bergeron by Kurt Vonnegut",
            Description = "Join us for a friendly discussion of the short story Harrison Bergeron by Kurt Vonnegut.",
            EventDate = DateTime.Now.AddDays(2),
            Venue = "Tech Hub",
            Address = "33 Sverdlovo Street",
            MaxParticipants = 10,
            Status = EventStatus.Planned
        };

        var defaultGroup = new Group
        {
            Name = "Reasoned Reads Round Table",
            Description = "Group created at initialization",
            Category = "Hobby",
            City = "Minsk",
            Country = "Belarus",
            Events = new List<Event> { defaultEvent }   
        };

        DataStorage.groups.Add(defaultGroup);
    }
}

