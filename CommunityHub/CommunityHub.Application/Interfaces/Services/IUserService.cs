using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Services;

public interface IUserService
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User> RegisterAsync(User user, string password);
    Task<bool> DeleteAsync(Guid id);
    Task<User> AuthenticateAsync(string username, string password);
}
