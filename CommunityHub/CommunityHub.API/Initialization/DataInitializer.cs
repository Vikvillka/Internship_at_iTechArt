using CommunityHub.API.DataSources;
using CommunityHub.API.Models;

namespace CommunityHub.API.Initialization;

public static class DataInitializer
{
    public static void Seed(ListDataSource<Community> dataSource)
    {
        // VS has refactoring too. I just missed the gray underline before, lol. Gotta pay more attention next time!)
        if (dataSource.GetAllAsync().Result.Count != 0) return;

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
      
        var defaultCommunity = new Community
        {
            Name = "Reasoned Reads Round Table",
            Description = "Group created at initialization",
            Category = "Hobby",
            City = "Minsk",
            Country = "Belarus",
            Events = [defaultEvent]
        };

        dataSource.CreateAsync(defaultCommunity).Wait();
    }
}

