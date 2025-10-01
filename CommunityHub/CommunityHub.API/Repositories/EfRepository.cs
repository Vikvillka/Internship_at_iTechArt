using Microsoft.EntityFrameworkCore;

using CommunityHub.API.Data;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;

namespace CommunityHub.API.Repositories;

public class EfRepository<T> : IRepository<T> where T : BaseEntity
{
    protected readonly CommunityHubDbContext _context;
    protected readonly DbSet<T> _dbSet;
    
    protected virtual IQueryable<T> CollectionWithIncludes => _dbSet;

    public EfRepository(CommunityHubDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await CollectionWithIncludes.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await CollectionWithIncludes.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> CreateAsync(T entity)
    {
       _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var existing = await _dbSet.FindAsync(entity.Id);
        if (existing == null) return false;
        
        _context.Entry(existing).CurrentValues.SetValues(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _dbSet.FindAsync(id);
        if (existing == null) return false;

        _dbSet.Remove(existing);
        await _context.SaveChangesAsync();
        return true;
    }

    public IQueryable<T> AsQueryable()
    {
        return CollectionWithIncludes;
    }
}

