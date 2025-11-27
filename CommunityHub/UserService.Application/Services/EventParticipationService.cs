using UserService.Application.Intarfaces.Repositories;
using UserService.Application.Intarfaces.Services;
using UserService.Domain.Entities;
using UserService.Domain.Exceptions;

namespace UserService.Application.Services;

public class EventParticipationService : IEventParticipationService
{
    private readonly IEventParticipationRepository _eventParticipationRepository;
    private readonly IUserService _userService;

    public EventParticipationService(IEventParticipationRepository eventParticipationRepository, IUserService userService)
    {
        _eventParticipationRepository = eventParticipationRepository;
        _userService = userService;
    }

    public async Task<IList<EventParticipation>> GetParticipationsByUserAsync(Guid userId)
    {
        await _userService.GetByUserIdAsync(userId);
        return await _eventParticipationRepository.GetByUserAsync(userId);
    }

    public async Task<EventParticipation> ParticipateAsync(Guid userId, Guid eventId)
    {
        await _userService.GetByUserIdAsync(userId);
        
        var existing = await _eventParticipationRepository.GetByUserAndEventAsync(userId, eventId);
        if (existing != null)
        {
            if (existing.IsConfirmed)
                throw new ConflictException("Conflict", "Already participating");

            await _eventParticipationRepository.UpdateStatusAsync(existing.Id, true);
            existing.IsConfirmed = true;
            return existing;
        }

        var participation = new EventParticipation
        {
            UserId = userId,
            EventId = eventId,
        };

        await _eventParticipationRepository.CreateAsync(participation);
        
        return participation;
    }

    public async Task<EventParticipation> CancelParticipationAsync(Guid userId, Guid eventId)
    {
        await _userService.GetByUserIdAsync(userId);
        
        var existing = await _eventParticipationRepository.GetByUserAndEventAsync(userId, eventId);
        if (existing == null || !existing.IsConfirmed)
            throw new NotFoundException("NotFound", "Participation not found");
       
        await _eventParticipationRepository.UpdateStatusAsync(existing.Id, false);
        existing.IsConfirmed = false;
        return existing;
    }
}
