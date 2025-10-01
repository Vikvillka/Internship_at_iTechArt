using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;

namespace CommunityHub.Application.Services;

public class EventService : IEventService 
{
    private readonly IEventRepository _eventRepository;
    private readonly ITagRepository _tagRepository;

    public EventService(IEventRepository eventRepository, ITagRepository tagRepository)
    {
        _eventRepository = eventRepository;
        _tagRepository = tagRepository;
    }

    public async Task<IList<Event>> GetAllPlannedAsync()
    {
        return await _eventRepository.GetAllPlannedAsync();
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        return await _eventRepository.GetByIdAsync(id);
    }

    public async Task<IList<Event>> GetByCommunityIdAsync(Guid communityId)
    {
        return await _eventRepository.GetByCommunityIdAsync(communityId);
    }

    public async Task<Event> CreateAsync(Event eventEntity, List<Guid> tagIds)
    {
        var tags = await _tagRepository.GetByIdsAsync(tagIds);
        eventEntity.Tags = tags;

        return await _eventRepository.CreateAsync(eventEntity);
    }

    public async Task<bool> UpdateAsync(Event eventEntity)
    {
        var existing = await _eventRepository.GetByIdAsync(eventEntity.Id);
        if (existing == null) return false;

        return await _eventRepository.UpdateAsync(eventEntity);
    }

    public async Task<bool> UpdateStatusAsync(Guid id, EventStatus newStatus)
    {
        var existing = await _eventRepository.GetByIdAsync(id);
        if (existing == null) return false;

        return await _eventRepository.UpdateStatusAsync(id, newStatus);
    }
}

