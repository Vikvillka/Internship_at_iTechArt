using Moq;
using UserService.Domain.Exceptions;
using UserService.Tests.Fixtures;

namespace UserService.Tests.ServicesTests.UserServiceTests;

public class UserServiceAuthenticateTests : IClassFixture<UserServiceTestFixture>
{
    private readonly UserServiceTestFixture _fixture;

    public UserServiceAuthenticateTests(UserServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "Authenticate")]
    [Theory]
    [InlineData("UserA", "Password123!")]
    [InlineData("UserB", "Password456!")]
    public async Task AuthenticateAsync_ShouldReturnUser_WhenCredentialsAreValid(string username, string password)
    {
        // Arrange
        var existingUser = _fixture.Users.First(u => u.Username == username);

        _fixture.MockRepo
            .Setup(r => r.GetByUsernameAsync(username))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _fixture.Service.AuthenticateAsync(username, password);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(existingUser.Username, result.Username);
        _fixture.MockRepo.Verify(r => r.GetByUsernameAsync(username), Times.Once);
    }

    [Trait("Method", "Authenticate")]
    [Theory]
    [InlineData("UserA", "WrongPass")]
    [InlineData("Imposter", "Password123!")]
    public async Task AuthenticateAsync_ShouldThrowException_WhenCredentialsAreInvalid(string username, string password)
    {
        // Arrange
        var existingUser = _fixture.Users.FirstOrDefault(u => u.Username == username);

        _fixture.MockRepo
            .Setup(r => r.GetByUsernameAsync(username))
            .ReturnsAsync(existingUser);

        // Act & Assert
        if (existingUser == null)
        {
            var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
                _fixture.Service.AuthenticateAsync(username, password));

            Assert.Equal("NotFound", ex.Error);
        }
        else
        {
            var ex = await Assert.ThrowsAsync<UnauthorizedException>(() =>
                _fixture.Service.AuthenticateAsync(username, password));

            Assert.Equal("Unauthorized", ex.Error);
        }

        _fixture.MockRepo.Verify(r => r.GetByUsernameAsync(username), Times.Once);
    }
}
