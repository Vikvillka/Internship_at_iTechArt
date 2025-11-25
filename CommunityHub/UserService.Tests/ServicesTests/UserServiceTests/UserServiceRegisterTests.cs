using Moq;

using UserService.Domain.Entities;
using UserService.Domain.Exceptions;
using UserService.Tests.Fixtures;

namespace UserService.Tests.ServicesTests.UserServiceTests;

public class UserServiceRegisterTests : IClassFixture<UserServiceTestFixture>
{
    private readonly UserServiceTestFixture _fixture;

    public UserServiceRegisterTests(UserServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "Register")]
    [Theory]
    [InlineData("NewUserA", "Pass123")]
    [InlineData("NewUserB", "Pass456")]
    public async Task RegisterAsync_ShouldCreateUser_WhenUsernameIsUnique(string username, string password)
    {
        // Arrange
        var newUser = new User { Username = username };

        _fixture.MockRepo
            .Setup(r => r.ExistsByUserNameAsync(username))
            .ReturnsAsync(false);

        _fixture.MockRepo
            .Setup(r => r.CreateAsync(It.Is<User>(u => u.Username == username)))
            .ReturnsAsync((User u) => u);

        // Act
        var result = await _fixture.Service.RegisterAsync(newUser, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result.Username);
        Assert.True(BCrypt.Net.BCrypt.Verify(password, result.PasswordHash));

        _fixture.MockRepo.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Once);
    }

    [Trait("Method", "Register")]
    [Theory]
    [InlineData("UserA", "Password123!")]
    [InlineData("UserB", "Password456!")]
    public async Task RegisterAsync_ShouldThrowConflictException_WhenUsernameAlreadyExists(string username, string password)
    {
        // Arrange
        var newUser = new User { Username = username };

        _fixture.MockRepo
            .Setup(r => r.ExistsByUserNameAsync(username))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ConflictException>(() =>
            _fixture.Service.RegisterAsync(newUser, password));

        Assert.Equal("Conflict", exception.Error);

        _fixture.MockRepo.Verify(r => r.CreateAsync(It.IsAny<User>()), Times.Never);
    }
}
