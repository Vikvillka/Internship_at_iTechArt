using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;

using Moq;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceGetByIdTests : IClassFixture<CommunityServiceTestFixture>
{
    private readonly CommunityServiceTestFixture _fixture;

    public CommunityServiceGetByIdTests(CommunityServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCommunityById_WhenExists()
    {
        // Arrange
        var targetId = _fixture.Communities[0].Id;
        var expectedCommunity = _fixture.Communities[0];

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(targetId))
            .ReturnsAsync(expectedCommunity);

        // Act
        var result = await _fixture.Service.GetByIdAsync(targetId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(targetId, result.Id);
        _fixture.MockRepo.Verify(r => r.GetByIdAsync(targetId), Times.Once);
    }

    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var exeption = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.GetByIdAsync(missingId)
        );

        Assert.Equal("NotFound", exeption.Error);
        _fixture.MockRepo.Verify(r => r.GetByIdAsync(missingId), Times.Once);
    }
}
