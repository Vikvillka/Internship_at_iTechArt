using CommunityHub.API.Data;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.API.Repositories;

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

