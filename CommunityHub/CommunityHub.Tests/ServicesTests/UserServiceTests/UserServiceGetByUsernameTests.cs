using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.UserServiceTests;

public class UserServiceGetByUsernameTests : IClassFixture<UserServiceTestFixture>
{
    private readonly UserServiceTestFixture _fixture;

    public UserServiceGetByUsernameTests(UserServiceTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Trait("Method", "GetByUsername")]
    [Theory]
    [InlineData("UserA")]
    [InlineData("UserB")]
    public async Task GetByUsernameAsync_ShouldReturnUser_WhenUserExists(string username)
    {
        // Arrange
        var existingUser = _fixture.Users.First(u => u.Username == username);

        _fixture.MockRepo
            .Setup(r => r.GetByUsernameAsync(username))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _fixture.Service.GetByUsernameAsync(username);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(username, result.Username);
        Assert.Equal(existingUser.PasswordHash, result.PasswordHash);
        _fixture.MockRepo.Verify(r => r.GetByUsernameAsync(username), Times.Once);
    }

    [Trait("Method", "GetByUsername")]
    [Theory]
    [InlineData("ImposterA")]
    [InlineData("ImposterB")]
    public async Task GetByUsernameAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist(string username)
    {
        // Arrange
        _fixture.MockRepo
            .Setup(r => r.GetByUsernameAsync(username))
            .ReturnsAsync((User?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.GetByUsernameAsync(username));

        Assert.Equal("NotFound", exception.Error);
        _fixture.MockRepo.Verify(r => r.GetByUsernameAsync(username), Times.Once);
    }
}
