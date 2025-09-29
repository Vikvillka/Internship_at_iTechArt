using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;
using System.Xml.Linq;

namespace CommunityHub.API.Initialization;

public static class DataInitializer
{
    public async static Task SeedAsync(ICommunityRepository communityRepo, ITagRepository tagRepo)
    {
        if ((await tagRepo.GetAllAsync()).Count != 0) return;

        var defaultTags = new List<string>
        {
            "Technology",
            "Health",
            "Education",
            "Entertainment",
            "Sports",
            "Business",
            "Art",
            "Science",
            "Travel",
            "Food" 
        };

        foreach (var name in defaultTags)
        {
            var tag = new EventTag { Name = name };
            await tagRepo.CreateAsync(tag);
        }

        if ((await communityRepo.GetAllAsync()).Count != 0) return;

        var communityId = Guid.NewGuid();

        var defaultEvent = new Event
        {
            Title = "Short Story Discussion: Harrison Bergeron by Kurt Vonnegut",
            Description = "Join us for a friendly discussion of the short story Harrison Bergeron by Kurt Vonnegut.",
            EventDate = DateTime.UtcNow.AddDays(2),
            Venue = "Tech Hub",
            Address = "33 Sverdlovo Street",
            MaxParticipants = 10,
            Status = EventStatus.Planned,
            CommunityId = communityId
        };

        var defaultCommunity = new Community
        {
            Id = communityId,
            Name = "Reasoned Reads Round Table",
            Description = "Group created at initialization",
            Category = "Hobby",
            City = "Minsk",
            Country = "Belarus",
            Events = [defaultEvent]
        };

        await communityRepo.CreateAsync(defaultCommunity);
    }
}

