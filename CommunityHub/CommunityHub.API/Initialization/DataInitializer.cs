using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;

namespace CommunityHub.API.Initialization;

public static class DataInitializer
{
    public async static Task SeedAsync(ICommunityRepository communityRepo, ITagRepository tagRepo)
    {
        if ((await tagRepo.GetAllAsync()).Count != 0) return;

        // TODO: I want to implement storing tags in a JSON file
        // because I plan to add a lot more and deserialize them here.
        // Is this a good idea?
        var defaultTags = new List<EventTag>
        {
            new() { Name = "Technology" },
            new() { Name = "Health" },
            new() { Name = "Education" },
            new() { Name = "Entertainment" },
            new() { Name = "Sports" },
            new() { Name = "Business" },
            new() { Name = "Art" },
            new() { Name = "Science" },
            new() { Name = "Travel" },
            new() { Name = "Food" }
        };

        foreach (var tag in defaultTags)
        {
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

