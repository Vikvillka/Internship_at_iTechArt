using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Services;

public interface ITagService
{
    Task<List<EventTag>> GetAllAsync();
    Task<List<EventTag>> GetByIdsAsync(List<Guid> tagIds);
}
