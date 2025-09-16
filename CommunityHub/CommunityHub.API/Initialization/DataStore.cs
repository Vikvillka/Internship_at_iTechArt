using CommunityHub.API.Models;

namespace CommunityHub.API.Initialization
{
    public static class DataStore
    {
        public static List<Group> Groups { get; } = new List<Group>();
    }
}
