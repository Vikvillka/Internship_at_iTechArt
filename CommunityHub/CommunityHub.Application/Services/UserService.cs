using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;

namespace CommunityHub.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User?> GetByUsernameAsync(string username)
    {
        var user = _userRepository.GetByUsernameAsync(username);
        if (user == null)
            throw new NotFoundException("NotFound", $"Community with username '{username}' not found");

        return user;
    }

    public async Task<User> RegisterAsync(User user, string password)
    {
        var existingUser = _userRepository.GetByUsernameAsync(user.Username);
        if (existingUser == null)
            throw new ConflictException("Conflict", $"Username '{user.Username}' is already taken");

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
}
