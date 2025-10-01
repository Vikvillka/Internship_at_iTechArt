using Microsoft.EntityFrameworkCore;

using CommunityHub.Domain.Entities;
using CommunityHub.Interfaces.Interfaces.Repositories;
using CommunityHub.Infrastructure.Data;

namespace CommunityHub.Infrastructure.Repositories;

public class TagRepository : EfRepository<EventTag>, ITagRepository
{
    public TagRepository(CommunityHubDbContext context) : base(context)
    {
    }

    public async Task<List<EventTag>> GetByIdsAsync(List<Guid> tagIds)
    {
        return await _dbSet.Where(t => tagIds.Contains(t.Id)).ToListAsync();
    }
}

