using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;

namespace CommunityHub.API.Repositories;

public class ListRepository<T> : IRepository<T> where T : Community
{
    public readonly List<T> Community = [];

    public async Task<List<T>> GetAllAsync() 
    { 
        return await Task.FromResult(Community.ToList()); 
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await Task.FromResult(Community.FirstOrDefault(c => c.Id == id));
    }

    public async Task<T> CreateAsync(T entity)
    {
        Community.Add(entity);
        return await Task.FromResult(entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var existing = Community.FirstOrDefault(c => c.Id == entity.Id);
        if (existing == null) return await Task.FromResult(false);

        existing.Name = entity.Name;
        existing.Description = entity.Description;
        existing.Category = entity.Category;
        existing.City = entity.City;
        existing.Country = entity.Country;
        existing.Events = entity.Events;

        return await Task.FromResult(true);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = Community.FirstOrDefault(c => c.Id == id);
        if (existing == null) return await Task.FromResult(false);

        Community.Remove(existing);
        return  await Task.FromResult(true);
    }

    public IQueryable<T> AsQueryable()
    {
        return Community.AsQueryable();
    }
}

