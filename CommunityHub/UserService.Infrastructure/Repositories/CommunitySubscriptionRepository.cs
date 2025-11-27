using Microsoft.EntityFrameworkCore;

using UserService.Application.Intarfaces.Repositories;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class CommunitySubscriptionRepository : EfRepository<CommunitySubscription>, ICommunitySubscriptionRepository
{
    public CommunitySubscriptionRepository(UserServiceDbContext context) : base(context)
    {
    }

    public async Task<CommunitySubscription?> GetByUserAndCommunityAsync(Guid userId, Guid communityId)
    {
        return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId && x.CommunityId == communityId);
    }

    public async Task<IList<CommunitySubscription>> GetByUserAsync(Guid userId)
    {
        var query = AsQueryable();

        if (userId != Guid.Empty)
            query = query.Where(x => x.UserId == userId);

        return await query.ToListAsync();
    }

    public async Task<IList<CommunitySubscription>> GetByCommunityAsync(Guid communityId)
    {
        var query = AsQueryable();

        if (communityId != Guid.Empty)
            query = query.Where(x => x.CommunityId == communityId);

        return await query.ToListAsync();
    }

    public async Task<bool> UpdateStatusAsync(Guid id, bool isActive)
    {
        var update = await _dbSet.Where(x => x.Id == id)
            .ExecuteUpdateAsync(u => u
            .SetProperty(x => x.IsActive, isActive));

        return update != 0;
    }
}
