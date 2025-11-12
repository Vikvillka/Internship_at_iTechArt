using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Services;

public interface IEventParticipationService
{
    Task ParticipateAsync(Guid userId, Guid eventId);
    Task CancelParticipationAsync(Guid userId, Guid eventId);
    Task<IList<EventParticipation>> GetParticipationsByUserAsync(Guid userId);
}
