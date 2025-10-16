using Moq;

using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;

namespace CommunityHub.Tests.ServicesTests.EventServiceTests;

public class EventServiceGetByIdTests : IClassFixture<EventServiceTestFixture>
{
    private readonly EventServiceTestFixture _fixture;

    public EventServiceGetByIdTests(EventServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldReturnEventById_WhenExists()
    {
        // Arrange
        var targetId = _fixture.Events[0].Id;
        var expectedEvent = _fixture.Events[0];

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(targetId))
            .ReturnsAsync(expectedEvent);

        // Act
        var result = await _fixture.Service.GetByIdAsync(targetId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(targetId, result.Id);
        _fixture.MockRepo.Verify(r => r.GetByIdAsync(targetId), Times.Once);
    }

    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenEventDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Event?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.GetByIdAsync(missingId)
        );

        Assert.Equal("NotFound", exception.Error);
        _fixture.MockRepo.Verify(r => r.GetByIdAsync(missingId), Times.Once);
    }
}
