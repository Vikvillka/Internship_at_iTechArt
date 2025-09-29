using Microsoft.EntityFrameworkCore;

using CommunityHub.API.Data;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;

namespace CommunityHub.API.Repositories;

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

    public async Task<bool> UpdateStatusAsync(Guid id, EventStatus newStatus)
    {
        var update = await _dbSet.Where(e => e.Id == id)
            .ExecuteUpdateAsync(u => u
            .SetProperty(e => e.Status, newStatus));

        return update != 0;
    }

    public async Task<Event> CreateWithTagsAsync(Event eventEntity, List<Guid> tagsIds)
    {
        // I couldn't get the names with attach, since EF already knows the EventTag context objects via Attach,
        // so it doesn't perform a repeat SELECT for other properties.
        var tags = await _context.Tags.Where(t => tagsIds.Contains(t.Id)).ToListAsync();
        eventEntity.Tags = tags;

        _dbSet.Add(eventEntity);
        await _context.SaveChangesAsync();
        
        return eventEntity;
    }
}