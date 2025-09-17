using CommunityHub.API.Data;
using CommunityHub.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityHub.API.DataSources;

public class EfDataSource<T> : IDataSource<T> where T : BaseEntity
{
    private readonly CommunityHubDbContext _context;

    public EfDataSource(CommunityHubDbContext context)
    {
        _context = context;
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        _context.Set<T>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var existing = await _context.Set<T>().FindAsync(entity.Id);
        if (existing == null) return false;
        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _context.Set<T>().FindAsync(id);
        if (existing == null) return false;
        if (existing is Community community)
        {
            community.IsDeleted = true;
            _context.Entry(community).State = EntityState.Modified;
        }
        else
        {
            _context.Set<T>().Remove(existing);
        }
        await _context.SaveChangesAsync();
        return true;
    }
}

