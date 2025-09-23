using CommunityHub.API.Data;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.API.Repositories;

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
