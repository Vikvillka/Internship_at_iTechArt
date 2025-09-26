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

    public async Task<List<Event>> GetAllPlannedAsync()
    {
        return await _dbSet.Where(e => e.Status == EventStatus.Planned).ToListAsync();
    }

    public async Task<List<Event>> GetByCommunityIdAsync(Guid communityId)
    {
        return await _dbSet.Where(e => e.CommunityId == communityId).ToListAsync();
    }

    public async Task<bool> UpdateStatusAsync(Guid id, EventStatus newStatus)
    {
        var update = await _dbSet.Where(e => e.Id == id)
            .ExecuteUpdateAsync(u => u
            .SetProperty(e => e.Status, newStatus));

        return update != 0;
    }
}