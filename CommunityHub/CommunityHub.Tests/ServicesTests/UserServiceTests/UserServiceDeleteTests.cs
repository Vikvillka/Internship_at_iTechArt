using Moq;

using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;

namespace CommunityHub.Tests.ServicesTests.UserServiceTests;

public class UserServiceDeleteTests : IClassFixture<UserServiceTestFixture>
{
    private readonly UserServiceTestFixture _fixture;

    public UserServiceDeleteTests(UserServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenUserExists()
    {
        // Arrange
        var existingUser = _fixture.Users[0];

        _fixture.MockRepo
            .Setup(r => r.GetByIdAsync(existingUser.Id))
            .ReturnsAsync(existingUser);

        _fixture.MockRepo
            .Setup(r => r.DeleteAsync(existingUser.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _fixture.Service.DeleteAsync(existingUser.Id);

        // Assert
        Assert.True(result);
        _fixture.MockRepo.Verify(r => r.DeleteAsync(existingUser.Id), Times.Once);
    }

    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenUserDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();

        _fixture.MockRepo
            .Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((User?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.DeleteAsync(missingId));

        Assert.Equal("NotFound", ex.Error);

        _fixture.MockRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
    }
}
