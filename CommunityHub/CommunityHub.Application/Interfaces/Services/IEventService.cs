using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;

namespace CommunityHub.Application.Interfaces.Services;

public interface IEventService
{
    Task<IList<Event>> GetAllPlannedAsync();
    Task<Event?> GetByIdAsync(Guid id);
    Task<IList<Event>> GetByCommunityIdAsync(Guid communityId);
    Task<Event> CreateAsync(Event eventEntity, List<Guid> tagIds);
    Task<bool> UpdateAsync(Event eventEntity);
    Task<bool> UpdateStatusAsync(Guid id, EventStatus newStatus);
}

