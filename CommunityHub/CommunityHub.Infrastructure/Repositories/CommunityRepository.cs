using Microsoft.EntityFrameworkCore;

using CommunityHub.Domain.Entities;
using CommunityHub.Interfaces.Interfaces.Repositories;
using CommunityHub.Infrastructure.Data;

namespace CommunityHub.Infrastructure.Repositories;

public class CommunityRepository: EfRepository<Community>, ICommunityRepository 
{
    public CommunityRepository(CommunityHubDbContext context) : base(context)
    {
    }

    protected override IQueryable<Community> CollectionWithIncludes => 
        _dbSet.Include(c => c.Events);

    public async Task<IList<Community>> SearchAsync(string? category, string? city, string? country)
    {
        var query = AsQueryable();

        if(!string.IsNullOrEmpty(category))
            query = query.Where(c => c.Category == category);
        if (!string.IsNullOrEmpty(city))
            query = query.Where(c => c.City == city);
        if (!string.IsNullOrWhiteSpace(country))
            query = query.Where(c => c.Country == country);

        return await query.ToListAsync();
    }
}
