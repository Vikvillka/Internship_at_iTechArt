using Microsoft.EntityFrameworkCore;

using UserService.Application.Intarfaces.Repositories;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class EventParticipationRepository : EfRepository<EventParticipation>, IEventParticipationRepository
{
    public EventParticipationRepository(UserServiceDbContext context) : base(context) { }

    public async Task<EventParticipation?> GetByUserAndEventAsync(Guid userId, Guid eventId)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.EventId == eventId);
    }

    public async Task<IList<EventParticipation>> GetByUserAsync(Guid userId)
    {
        var query = AsQueryable();

        if (userId != Guid.Empty)
            query = query.Where(x => x.UserId == userId);

        return await query.ToListAsync();
    }

    public async Task<IList<EventParticipation>> GetByEventAsync(Guid eventId)
    {
        var query = AsQueryable();

        if (eventId != Guid.Empty)
            query = query.Where(x => x.EventId == eventId);

        return await query.ToListAsync();
    }

    public async Task<bool> UpdateStatusAsync(Guid id, bool isConfirmed)
    {
        var update = await _dbSet
            .Where(x => x.Id == id)
            .ExecuteUpdateAsync(u => u.SetProperty(x => x.IsConfirmed, isConfirmed));

        return update != 0;
    }

    public async Task<int> GetEventParticipantsCountAsync(Guid eventId)
    {
        return await _dbSet
            .Where(p => p.EventId == eventId && p.IsConfirmed)
            .CountAsync();
    }
}
