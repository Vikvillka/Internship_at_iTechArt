using CommunityHub.Domain.Entities;

namespace CommunityHub.Interfaces.Interfaces.Repositories;

public interface ITagRepository : IRepository<EventTag>
{
    Task<List<EventTag>> GetByIdsAsync(List<Guid> tagIds);
}
