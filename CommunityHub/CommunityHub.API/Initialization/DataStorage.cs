using CommunityHub.API.Models;

namespace CommunityHub.API.Initialization;

public static class DataStorage
{
    public static List<Group> groups { get; } = new List<Group>();

    public static List<Group> GetAllGroups() => groups;

    public static Group? GetGroupById(Guid id) => groups.FirstOrDefault(g => g.Id == id);

    public static void AddGroup(Group group)
    {
        groups.Add(group);
    }

    public static bool UpdateGroup(Group updatedGroup)
    {
        var existingGroup = groups.FirstOrDefault(g => g.Id == updatedGroup.Id);
        if (existingGroup == null) return false;

        existingGroup.Name = updatedGroup.Name;
        existingGroup.Description = updatedGroup.Description;
        existingGroup.Category = updatedGroup.Category;
        existingGroup.City = updatedGroup.City;
        existingGroup.Country = updatedGroup.Country;
        existingGroup.Events = updatedGroup.Events;

        return true;
    }

    public static bool DeleteGroup(Guid id)
    {
        var group = groups.FirstOrDefault(g => g.Id == id);
        if (group == null) return false;
            
        groups.Remove(group);
        return true;
    }
}

