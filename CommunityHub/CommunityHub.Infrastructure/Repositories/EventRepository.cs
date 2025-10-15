using Microsoft.EntityFrameworkCore;

using CommunityHub.Domain.Entities;
using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Infrastructure.Data;
using CommunityHub.Domain.Enums;

namespace CommunityHub.Infrastructure.Repositories;

public class EventRepository : EfRepository<Event>, IEventRepository
{
    public EventRepository(CommunityHubDbContext context) : base(context)
    {
    }

    protected override IQueryable<Event> CollectionWithIncludes =>
        _dbSet.Include(e => e.Tags);

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
}