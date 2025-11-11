using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Services;

public interface IUserService
{
    Task<User?> GetByUserIdAsync(Guid userId);
    Task<User?> GetByUsernameAsync(string username);
    Task<User> RegisterAsync(User user, string password);
    Task<bool> DeleteAsync(Guid id);
    Task<User> AuthenticateAsync(string username, string password);
    Task<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids);
}
