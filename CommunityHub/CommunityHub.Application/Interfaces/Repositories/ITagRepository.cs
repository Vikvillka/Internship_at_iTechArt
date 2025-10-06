using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Repositories;

public interface ITagRepository : IRepository<EventTag>
{
    Task<IList<EventTag>> GetByIdsAsync(List<Guid> tagIds);
}
