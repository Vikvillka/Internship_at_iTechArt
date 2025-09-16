using CommunityHub.API.Models;

namespace CommunityHub.API.Initialization;

public static class DataStorage
{
    public static List<Community> Community { get; } = new List<Community>();

    public static List<Community> GetAllCommunities() => Community;

    public static Community? GetCommunityById(Guid id) => Community.FirstOrDefault(g => g.Id == id);

    public static void AddCommunity(Community community)
    {
        Community.Add(community);
    }

    public static bool UpdateCommunity(Community updatedCommunity)
    {
        var existingCommunity = Community.FirstOrDefault(g => g.Id == updatedCommunity.Id);
        if (existingCommunity == null) return false;

        existingCommunity.Name = updatedCommunity.Name;
        existingCommunity.Description = updatedCommunity.Description;
        existingCommunity.Category = updatedCommunity.Category;
        existingCommunity.City = updatedCommunity.City;
        existingCommunity.Country = updatedCommunity.Country;
        existingCommunity.Events = updatedCommunity.Events;

        return true;
    }

    public static bool DeleteCommunity(Guid id)
    {
        var community = Community.FirstOrDefault(g => g.Id == id);
        if (community == null) return false;
            
        Community.Remove(community);
        return true;
    }
}

