using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using CommunityHub.Domain.Exceptions;

namespace CommunityHub.Application.Services;

public class EventService : IEventService 
{
    private readonly IEventRepository _eventRepository;
    private readonly ITagService _tagService;

    public EventService(IEventRepository eventRepository, ITagService tagService)
    {
        _eventRepository = eventRepository;
        _tagService = tagService;
    }

    public async Task<IList<Event>> GetAllPlannedAsync()
    {
        return await _eventRepository.GetAllPlannedAsync();
    }

    public async Task<Event?> GetByIdAsync(Guid id)
    {
        var eventEntity = await _eventRepository.GetByIdAsync(id);
        if (eventEntity == null)
            throw new NotFoundException("NotFound", $"Event with id '{id}' not found");
        return eventEntity;
    }

    public async Task<IList<Event>> GetByCommunityIdAsync(Guid communityId)
    {
        return await _eventRepository.GetByCommunityIdAsync(communityId);
    }

    public async Task<Event> CreateAsync(Event eventEntity, List<Guid> tagIds)
    {
        var tags = await _tagService.GetByIdsAsync(tagIds);
        eventEntity.Tags = tags;

        return await _eventRepository.CreateAsync(eventEntity);
    }

    public async Task<bool> UpdateAsync(Event eventEntity)
    {
        var existing = await _eventRepository.GetByIdAsync(eventEntity.Id);
        if (existing == null)
            throw new NotFoundException("NotFound", $"Event with id '{eventEntity.Id}' not found");

        return await _eventRepository.UpdateAsync(eventEntity);
    }

    public async Task<bool> UpdateStatusAsync(Guid id, EventStatus newStatus)
    {
        var existing = await _eventRepository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException("NotFound", $"Event with id '{id}' not found");

        return await _eventRepository.UpdateStatusAsync(id, newStatus);
    }
}

