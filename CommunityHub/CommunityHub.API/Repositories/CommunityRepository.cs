using CommunityHub.API.Data;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.API.Repositories;

public class CommunityRepository<T> : EfRepository<Community>, ICommunityRepository<Community> where T : Community
{
    public CommunityRepository(CommunityHubDbContext context) : base(context)
    {
    }

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

    public override async Task<List<Community>> GetAllAsync()
    {
        return await _context.Set<Community>().Include(c => c.Events).ToListAsync();
    }

    public override async Task<Community?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Community>()
            .Include(c => c.Events)
            .FirstOrDefaultAsync(c => c.Id == id);
    }
}
