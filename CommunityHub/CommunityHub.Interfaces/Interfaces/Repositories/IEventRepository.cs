using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;

namespace CommunityHub.Interfaces.Interfaces.Repositories;

public interface IEventRepository : IRepository<Event>
{
    Task<List<Event>> GetAllPlannedAsync();
    Task<List<Event>> GetByCommunityIdAsync(Guid communityId);
    Task<bool> UpdateStatusAsync(Guid eventId, EventStatus newStatus);
}
