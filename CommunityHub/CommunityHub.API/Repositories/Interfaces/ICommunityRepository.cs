using CommunityHub.API.Models;

namespace CommunityHub.API.Repositories.Interfaces;

public interface ICommunityRepository<T> : IRepository<Community>
{
    Task<IList<Community>> SearchAsync(string? category, string? city, string? country);
}