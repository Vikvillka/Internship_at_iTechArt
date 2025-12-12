using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Contracts.DTOs.EventDTOs;
using CommunityHub.Domain.Common;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using CommunityHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.Infrastructure.Repositories;

public class EventRepository : EfRepository<Event>, IEventRepository
{
    public EventRepository(CommunityHubDbContext context) : base(context)
    {
    }

    protected override IQueryable<Event> CollectionWithIncludes =>
        _dbSet.Include(e => e.Tags).Include(e => e.Community);

    public async Task<List<Event>> GetAllPlannedAsync()
    {
        return await CollectionWithIncludes.Where(e => e.Status == EventStatus.Planned).ToListAsync();
    }

    public async Task<List<Event>> GetByCommunityIdAsync(Guid communityId)
    {
        return await CollectionWithIncludes.Where(e => e.CommunityId == communityId).ToListAsync();
    }

    public async Task<bool> ExistsWithSameTitleAndTimeAsync(Guid communityId, string title, DateTime eventDate)
    {
        return await _dbSet.AnyAsync(e =>
            e.CommunityId == communityId &&
            e.Title == title &&
            e.EventDate == eventDate);
    }

    public async Task<bool> UpdateStatusAsync(Guid id, EventStatus newStatus)
    {
        var update = await _dbSet.Where(e => e.Id == id)
            .ExecuteUpdateAsync(u => u
            .SetProperty(e => e.Status, newStatus));

        return update != 0;
    }

    public async Task<PagedResult<Event>> GetEventsBySearchAsync(EventSearchRequest request)
    {
        var query = CollectionWithIncludes.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Keywords))
        {
            var keywords = request.Keywords.ToLower();
            query = query.Where(e =>
                e.Title.ToLower().Contains(keywords) ||
                e.Description.ToLower().Contains(keywords));
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
            query = query.Where(e => e.Community.Category == request.Category);

        if (!string.IsNullOrWhiteSpace(request.City))
            query = query.Where(e => e.Community.City == request.City);

        if (!string.IsNullOrWhiteSpace(request.Country))
            query = query.Where(e => e.Community.Country == request.Country);

        if (request.DateFrom.HasValue)
            query = query.Where(e => e.EventDate >= request.DateFrom.Value);

        if (request.DateTo.HasValue)
            query = query.Where(e => e.EventDate <= request.DateTo.Value);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Event>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}