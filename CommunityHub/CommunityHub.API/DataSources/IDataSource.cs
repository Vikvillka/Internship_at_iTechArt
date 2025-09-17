using CommunityHub.API.Models;

namespace CommunityHub.API.DataSources;

public interface IDataSource<T> where T : BaseEntity
{
    // for now I will leave it like this, I might consider implementing separate method returning IQueryable<T> for EF LINQ queries in the future
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
}
