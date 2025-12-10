using CommunityHub.Contracts.DTOs.EventDTOs;
using CommunityHub.Domain.Common;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;

namespace CommunityHub.Application.Interfaces.Repositories;

public interface IEventRepository : IRepository<Event>
{
    Task<List<Event>> GetAllPlannedAsync();
    Task<List<Event>> GetByCommunityIdAsync(Guid communityId);
    Task<bool> ExistsWithSameTitleAndTimeAsync(Guid communityId, string title, DateTime eventDate);
    Task<bool> UpdateStatusAsync(Guid eventId, EventStatus newStatus);
    Task<PagedResult<Event>> GetEventsBySearchAsync(EventSearchRequest request);
}
