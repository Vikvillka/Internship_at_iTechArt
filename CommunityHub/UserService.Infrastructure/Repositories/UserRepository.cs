using Microsoft.EntityFrameworkCore;

using UserService.Application.Intarfaces.Repositories;
using UserService.Domain.Entities;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;

public class UserRepository : EfRepository<User>, IUserRepository
{
    public UserRepository(UserServiceDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
    }

    public async Task<bool> ExistsByEmailAsync(string email) =>
       await _dbSet.AnyAsync(u => u.Email == email);

    public async Task<bool> ExistsByUserNameAsync(string username) =>
        await _dbSet.AnyAsync(u => u.Username == username);

    public async Task<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        var query = AsQueryable();

        if (ids != null && ids.Any())
            query = query.Where(u => ids.Contains(u.Id));

        return await query.ToListAsync();
    }
}
