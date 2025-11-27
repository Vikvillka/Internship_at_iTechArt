using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> ExistsByEmailAsync(string email);
    Task<bool> ExistsByUserNameAsync(string username);
    Task<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids);
}
