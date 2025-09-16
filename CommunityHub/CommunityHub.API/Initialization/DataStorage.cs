using CommunityHub.API.Models;

namespace CommunityHub.API.Initialization;

public static class DataStorage
{
    public static List<Community> Community { get; } = new List<Community>();

    public static List<Community> GetAllCommunities() => Community;

    public static Community? GetCommunityById(Guid id) => Community.FirstOrDefault(g => g.Id == id);

    public static void AddCommunity(Community group)
    {
        Community.Add(group);
    }

    public static bool UpdateCommunity(Community updatedGroup)
    {
        var existingGroup = Community.FirstOrDefault(g => g.Id == updatedGroup.Id);
        if (existingGroup == null) return false;

        existingGroup.Name = updatedGroup.Name;
        existingGroup.Description = updatedGroup.Description;
        existingGroup.Category = updatedGroup.Category;
        existingGroup.City = updatedGroup.City;
        existingGroup.Country = updatedGroup.Country;
        existingGroup.Events = updatedGroup.Events;

        return true;
    }

    public static bool DeleteCommunity(Guid id)
    {
        var group = Community.FirstOrDefault(g => g.Id == id);
        if (group == null) return false;
            
        Community.Remove(group);
        return true;
    }
}

