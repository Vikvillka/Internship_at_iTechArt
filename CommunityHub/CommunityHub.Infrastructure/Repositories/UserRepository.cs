using Microsoft.EntityFrameworkCore;

using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Domain.Entities;
using CommunityHub.Infrastructure.Data;

namespace CommunityHub.Infrastructure.Repositories;

public class UserRepository : EfRepository<User>, IUserRepository
{
    public UserRepository(CommunityHubDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }
}
