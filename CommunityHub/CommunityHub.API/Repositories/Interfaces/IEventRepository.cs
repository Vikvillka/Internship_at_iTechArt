using CommunityHub.API.Models;

namespace CommunityHub.API.Repositories.Interfaces;

public interface IEventRepository : IRepository<Event>
{
    Task<List<Event>> GetAllPlannedAsync();
    Task<List<Event>> GetByCommunityIdAsync(Guid communityId);
    Task<bool> UpdateStatusAsync(Guid eventId, EventStatus newStatus);
}
