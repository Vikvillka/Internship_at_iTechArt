using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Services;

public interface ITagService
{
    Task<IList<EventTag>> GetAllAsync();
    Task<IList<EventTag>> GetByIdsAsync(List<Guid> tagIds);
}
