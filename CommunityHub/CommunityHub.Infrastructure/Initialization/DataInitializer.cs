using CommunityHub.Domain.Entities;
using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Domain.Enums;

namespace CommunityHub.Infrastructure.Initialization;

public static class DataInitializer
{
    public async static Task SeedAsync(ICommunityRepository communityRepo, ITagRepository tagRepo)
    {
        if ((await tagRepo.GetAllAsync()).Count != 0) return;

        var defaultTags = new List<string>
        {
                "Technology", "Health", "Education", "Entertainment", "Sports",
                "Business", "Art", "Science", "Travel", "Food",

                "Photography", "Gaming", "Cooking", "Reading", "Board Games",
                "Gardening", "DIY", "Pets", "Handmade", "Movies",

                "Programming", "AI & Machine Learning", "Cybersecurity",
                "Web Development", "Cloud Computing", "Startups", "Investing",

                "Running", "Yoga", "Cycling", "Football", "Basketball",
                "Martial Arts", "Gym", "Swimming",

                "Networking", "Volunteering", "Language Exchange",
                "Public Speaking", "Community Building",

                "Hiking", "Camping", "Backpacking", "Road Trips"
        };

        foreach (var name in defaultTags)
        {
            var tag = new EventTag { Name = name };
            await tagRepo.CreateAsync(tag);
        }

        if ((await communityRepo.GetAllAsync()).Count != 0) return;

        var tags = await tagRepo.GetAllAsync();

        var communityHobbyId = Guid.NewGuid();

        var hobbyEvent = new Event
        {
            Title = "Classic Literature Evening: Discussing George Orwell’s '1984'",
            Description =
                "Join a warm and welcoming literary evening dedicated to George Orwell’s iconic dystopian novel '1984'. " +
                "We explore not only the political and philosophical themes of the book, but also its relevance in the modern world. " +
                "Participants are encouraged to share personal reflections, interpretations, favorite quotes and comparisons with real events. " +
                "This session is designed for both avid readers and newcomers who want to enjoy meaningful dialogue, exchange viewpoints, " +
                "and spend an inspiring evening in a cozy intellectual atmosphere.",
            EventDate = DateTime.UtcNow.AddDays(6),
            Venue = "Kulturhaus Berlin",
            Address = "Friedrichstraße 134, Berlin",
            MaxParticipants = 20,
            Duration = 150,
            Status = EventStatus.Planned,
            Latitude = 52.5200,
            Longitude = 13.4050,
            CommunityId = communityHobbyId,
            Tags = tags.Where(t =>
                t.Name is "Reading" or "Education" or "Art" or "Entertainment" or "Movies"
            ).ToList()
        };

        var communityHobby = new Community
        {
            Id = communityHobbyId,
            Name = "Berlin International Book Circle",
            Description =
                "A multicultural book community uniting passionate readers from around the world. " +
                "We organize themed literary evenings, large-format discussions, author spotlights, and interactive storytelling events. " +
                "Our mission is to create a friendly environment where people can share ideas, discover new books, and build long-lasting connections.",
            Category = "Hobby",
            City = "Berlin",
            Country = "Germany",
            Events = [hobbyEvent]
        };

        var communitySportsId = Guid.NewGuid();

        var sportsEvent = new Event
        {
            Title = "Central Park Sunrise Run – 5km Group Session",
            Description =
                "A refreshing early morning running session for athletes of all levels. " +
                "We meet at 6:30 AM near the Columbus Circle entrance and begin with a 10-minute stretching routine, " +
                "followed by a comfortable 5km run through scenic Central Park. " +
                "The pace will be friendly and moderate, allowing everyone to participate regardless of experience. " +
                "After the run, we gather for a short cooldown and optional healthy breakfast at a nearby café.",
            EventDate = DateTime.UtcNow.AddDays(3),
            Venue = "Central Park",
            Address = "Columbus Circle, New York",
            MaxParticipants = 40,
            Duration = 90,
            Status = EventStatus.Planned,
            Latitude = 40.7831,
            Longitude = -73.9712,
            CommunityId = communitySportsId,
            Tags = tags.Where(t =>
                t.Name is "Running" or "Sports" or "Health" or "Fitness" or "Cycling"
            ).ToList()
        };

        var communitySports = new Community
        {
            Id = communitySportsId,
            Name = "NYC Active Lifestyle Crew",
            Description =
                "A vibrant sports community based in New York City, organizing weekly runs, cycling tours, yoga meetups, " +
                "functional training sessions, and outdoor activities for people who want to stay fit and socialize. " +
                "Members support each other, share progress, set goals, and participate in friendly challenges.",
            Category = "Sports",
            City = "New York",
            Country = "USA",
            Events = [sportsEvent]
        };

        var communityTechId = Guid.NewGuid();

        var techEvent = new Event
        {
            Title = "AI & Machine Learning Bootcamp – Hands-On for Beginners",
            Description =
                "A practical and immersive introduction to AI and machine learning. " +
                "This 3-hour workshop covers fundamental ML concepts, data preparation methods, model training, and real-time experiments. " +
                "Participants will build simple neural networks, test algorithms, and explore modern tools used by professionals. " +
                "No prior experience is required — the session is structured to be beginner-friendly while still valuable for intermediate learners. " +
                "Networking session with startup founders and developers included.",
            EventDate = DateTime.UtcNow.AddDays(10),
            Venue = "Tokyo Tech Lab",
            Address = "Shibuya 2-24-12, Tokyo",
            MaxParticipants = 50,
            Duration = 180,
            Status = EventStatus.Planned,
            Latitude = 35.6595,
            Longitude = 139.7005,
            CommunityId = communityTechId,
            Tags = [.. tags.Where(t =>
                t.Name is "Technology" or "Programming" or "AI & Machine Learning"
                or "Startups" or "Cybersecurity"
            )]
        };

        var communityTech = new Community
        {
            Id = communityTechId,
            Name = "Tokyo Future Tech Innovators",
            Description =
                "A technology community that brings together engineers, developers, designers, researchers, and startup enthusiasts. " +
                "We host meetups, coding sessions, workshops, tech talks, and hackathons focusing on cutting-edge technologies. " +
                "Our goal is to build a collaborative space where people can exchange knowledge and create meaningful tech projects.",
            Category = "Technology",
            City = "Tokyo",
            Country = "Japan",
            Events = [techEvent]
        };

        await communityRepo.CreateAsync(communityHobby);
        await communityRepo.CreateAsync(communitySports);
        await communityRepo.CreateAsync(communityTech);
    }
}

