using CommunityHub.API.Models;

namespace CommunityHub.API.DataSources;

public class ListDataSource<T> : IDataSource<T> where T : Community
{
    public readonly List<T> Community = [];

    public Task<List<T>> GetAllAsync() => Task.FromResult(Community.ToList());

    public Task<T?> GetByIdAsync(Guid id) =>
        Task.FromResult(Community.FirstOrDefault(c => c.Id == id));

    public Task<T> CreateAsync(T entity)
    {
        Community.Add(entity);
        return Task.FromResult(entity);
    }

    public Task<bool> UpdateAsync(T entity)
    {
        var existing = Community.FirstOrDefault(c => c.Id == entity.Id);
        if (existing == null) return Task.FromResult(false);

        existing.Name = entity.Name;
        existing.Description = entity.Description;
        existing.Category = entity.Category;
        existing.City = entity.City;
        existing.Country = entity.Country;
        existing.Events = entity.Events;

        return Task.FromResult(true);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var existing = Community.FirstOrDefault(c => c.Id == id);
        if (existing == null) return Task.FromResult(false);

        Community.Remove(existing);
        return Task.FromResult(true);
    }
}

