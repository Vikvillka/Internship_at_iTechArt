namespace HistoryService.Application.Intefaces.Repositories;

public interface IRepository<T> 
{
    Task<List<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateAsync(T entity);
    Task<bool> DeleteAsync(Guid id);
    IQueryable<T> AsQueryable();
}