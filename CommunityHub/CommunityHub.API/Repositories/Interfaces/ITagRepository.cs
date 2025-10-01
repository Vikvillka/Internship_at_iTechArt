using CommunityHub.API.Models;

namespace CommunityHub.API.Repositories.Interfaces;

public interface ITagRepository : IRepository<EventTag>
{
    Task<List<EventTag>> GetByIdsAsync(List<Guid> tagIds);
}
