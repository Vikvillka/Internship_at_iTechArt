using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using CommunityHub.Domain.Exceptions;

namespace CommunityHub.Application.Services;

public class EventService : IEventService 
{
    private readonly IEventRepository _eventRepository;
    private readonly ICommunityService _communityService;
    private readonly ITagService _tagService;

    public EventService(IEventRepository eventRepository, ITagService tagService, ICommunityService communityService)
    {
        _eventRepository = eventRepository;
        _tagService = tagService;
        _communityService = communityService;
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
        var community = await _communityService.GetByIdAsync(communityId);
        if (community == null)
            throw new NotFoundException("NotFound", $"Community with id '{communityId}' not found");

        return await _eventRepository.GetByCommunityIdAsync(communityId);
    }

    public async Task<Event> CreateAsync(Event eventEntity, List<Guid> tagIds)
    {
        await EnsureUniqueEventTitleAndTimeAsync(eventEntity);

        var tags = await _tagService.GetByIdsAsync(tagIds);
        eventEntity.Tags = tags;
        eventEntity.Status = EventStatus.Planned;

        return await _eventRepository.CreateAsync(eventEntity);
    }

    public async Task<bool> UpdateAsync(Event eventEntity)
    {
        var existing = await _eventRepository.GetByIdAsync(eventEntity.Id);
        if (existing == null)
            throw new NotFoundException("NotFound", $"Event with id '{eventEntity.Id}' not found");

        await EnsureUniqueEventTitleAndTimeAsync(eventEntity, eventEntity.Id);
        return await _eventRepository.UpdateAsync(eventEntity);
    }

    public async Task<bool> UpdateStatusAsync(Guid id, EventStatus newStatus)
    {
        var existing = await _eventRepository.GetByIdAsync(id);
        if (existing == null)
            throw new NotFoundException("NotFound", $"Event with id '{id}' not found");

        return await _eventRepository.UpdateStatusAsync(id, newStatus);
    }

    private async Task EnsureUniqueEventTitleAndTimeAsync(Event eventEntity, Guid? excludeId = null)
    {
        var exists = await _eventRepository.ExistsWithSameTitleAndTimeAsync(
            eventEntity.CommunityId,
            eventEntity.Title,
            eventEntity.EventDate);

        if (!exists)
            return;
        
        if (excludeId != null)
        {
            var currentEvent = await _eventRepository.GetByIdAsync(excludeId.Value);
            if (currentEvent != null &&
                currentEvent.Title == eventEntity.Title &&
                currentEvent.EventDate == eventEntity.EventDate)
            {
                return; 
            }
        }

        throw new ConflictException("Conflict", $"Event with title '{eventEntity.Title}' and time '{eventEntity.EventDate}' is already taken");
    }
}

