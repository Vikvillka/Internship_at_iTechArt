using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceDeleteTests : IClassFixture<CommunityServiceTestFixture>
{
    private readonly CommunityServiceTestFixture _fixture;

    public CommunityServiceDeleteTests(CommunityServiceTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenCommunityExists()
    {
        // Arrange
        var existing = _fixture.Communities[0];

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(existing.Id))
            .ReturnsAsync(existing);
        _fixture.MockRepo.Setup(r => r.DeleteAsync(existing.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _fixture.Service.DeleteAsync(existing.Id);

        // Assert
        Assert.True(result);
    }

    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.DeleteAsync(missingId));

        Assert.Equal("NotFound", ex.Error);
    }
}
