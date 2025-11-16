using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Services;

public interface IEventParticipationService
{
    Task<EventParticipation> ParticipateAsync(Guid userId, Guid eventId);
    Task<EventParticipation> CancelParticipationAsync(Guid userId, Guid eventId);
    Task<IList<EventParticipation>> GetParticipationsByUserAsync(Guid userId);
}
