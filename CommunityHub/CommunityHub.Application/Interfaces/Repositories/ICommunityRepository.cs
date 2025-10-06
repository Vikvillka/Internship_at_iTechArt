using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Repositories;

public interface ICommunityRepository : IRepository<Community>
{
    Task<IList<Community>> SearchAsync(string? category, string? city, string? country);
}