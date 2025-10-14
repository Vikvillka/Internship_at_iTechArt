using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
}
