using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Domain.Common;
using CommunityHub.Domain.Entities;
using CommunityHub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.Infrastructure.Repositories;

public class CommunityRepository: EfRepository<Community>, ICommunityRepository 
{
    public CommunityRepository(CommunityHubDbContext context) : base(context)
    {
    }

    protected override IQueryable<Community> CollectionWithIncludes => 
        _dbSet.Include(c => c.Events);

    public async Task<IList<Community>> FindCommunitiesAsync(string? category, string? city, string? country)
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

    public async Task<PagedResult<Community>> GetCommunitiesBySearchAsync(CommunitySearchRequest request)
    {
        var query = _dbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Keywords))
        {
            var keyword = request.Keywords.ToLower();
            query = query.Where(c =>
                c.Name.ToLower().Contains(keyword) ||
                c.Description.ToLower().Contains(keyword));
        }

        if (!string.IsNullOrWhiteSpace(request.Category))
            query = query.Where(c => c.Category == request.Category);

        if (!string.IsNullOrWhiteSpace(request.City))
            query = query.Where(c => c.City == request.City);

        if (!string.IsNullOrWhiteSpace(request.Country))
            query = query.Where(c => c.Country == request.Country);

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return new PagedResult<Community>
        {
            Items = items,
            TotalCount = totalCount,
            Page = request.Page,
            PageSize = request.PageSize
        };
    }
}
