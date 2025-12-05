using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Repositories;

public interface IEventParticipationRepository : IRepository<EventParticipation>
{
    Task<EventParticipation?> GetByUserAndEventAsync(Guid userId, Guid eventId);
    Task<IList<EventParticipation>> GetByUserAsync(Guid userId);
    Task<IList<EventParticipation>> GetByEventAsync(Guid eventId);
    Task<bool> UpdateStatusAsync(Guid id, bool isConfirmed);
    Task<int> GetEventParticipantsCountAsync(Guid eventId);
}
