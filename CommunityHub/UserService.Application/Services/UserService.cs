using UserService.Application.Intarfaces.Repositories;
using UserService.Application.Intarfaces.Services;
using UserService.Domain.Entities;
using UserService.Domain.Exceptions;

namespace UserService.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IList<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetByUserIdAsync(Guid userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if(user == null)
            throw new NotFoundException("NotFound", $"User with id '{userId}' not found");

        return user;
    } 

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
            throw new NotFoundException("NotFound", $"User with username '{username}' not found");

        return user;
    }

    public async Task<User> RegisterAsync(User user, string password)
    {
        var existingUsername = await _userRepository.ExistsByUserNameAsync(user.Username);
        if (existingUsername)
            throw new ConflictException("Conflict", $"Username '{user.Username}' is already taken");

        var existingEmail = await _userRepository.ExistsByEmailAsync(user.Email);
        if (existingEmail)
            throw new ConflictException("Conflict", $"Email '{user.Email}' is already taken");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

        return await _userRepository.CreateAsync(user);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingUser = await _userRepository.GetByIdAsync(id);
        if (existingUser == null)
            throw new NotFoundException("NotFound", $"User with id '{id}' not found");

        return await _userRepository.DeleteAsync(id);
    }

    public async Task<User> AuthenticateAsync(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        if (user == null)
            throw new NotFoundException("NotFound", $"User with username '{username}' not found");
        
        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            throw new UnauthorizedException("Unauthorized", "Invalid username or password");
        
        return user;
    }

    public async Task<IList<User>> GetByIdsAsync(IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any())
            throw new BadRequestException("BadRequest", "User IDs list cannot be empty");

        var users = await _userRepository.GetByIdsAsync(ids);

        if (users == null || users.Count == 0)
            throw new NotFoundException("NotFound", "No users found for provided IDs");

        return users;
    }
}
