using CommunityHub.API.DataSources;
using CommunityHub.API.Models;

namespace CommunityHub.API.Initialization;

public static class DataInitializer
{
    public async static Task SeedAsync(IRepository<Community> dataSource)
    {
        if ((await dataSource.GetAllAsync()).Count != 0) return;

        var defaultEvent = new Event
        {
            Title = "Short Story Discussion: Harrison Bergeron by Kurt Vonnegut",
            Description = "Join us for a friendly discussion of the short story Harrison Bergeron by Kurt Vonnegut.",
            EventDate = DateTime.UtcNow.AddDays(2),
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

        await dataSource.CreateAsync(defaultCommunity);
    }
}

