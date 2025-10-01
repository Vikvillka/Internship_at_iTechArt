using CommunityHub.Domain.Entities;

namespace CommunityHub.Interfaces.Interfaces.Repositories;

public interface IRepository<T> where T : BaseEntity
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    IQueryable<T> AsQueryable();
}