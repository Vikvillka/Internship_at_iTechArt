using Moq;
using UserService.Application.Intarfaces.Repositories;
using UserService.Domain.Entities;

namespace UserService.Tests.Fixtures;

public class UserServiceTestFixture
{
    public UserService.Application.Services.UserService Service { get; }
    public Mock<IUserRepository> MockRepo { get; }
    public List<User> Users { get; }

    public UserServiceTestFixture()
    {
        MockRepo = new Mock<IUserRepository>();
        Service = new UserService.Application.Services.UserService(MockRepo.Object);

        var password1 = "Password123!";
        var password2 = "Password456!";

        Users =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Username = "UserA",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password1)
            },
            new()
            {
                Id = Guid.NewGuid(),
                Username = "UserB",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password2)
            }
        ];
    }
}
