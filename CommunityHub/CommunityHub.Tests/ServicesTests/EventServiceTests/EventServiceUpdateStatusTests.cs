using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.EventServiceTests;

public class EventServiceUpdateStatusTests : IClassFixture<EventServiceTestFixture>
{
    private readonly EventServiceTestFixture _fixture;

    public EventServiceUpdateStatusTests(EventServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "UpdateStatus")]
    [Fact]
    public async Task UpdateStatusAsync_ShouldReturnTrue_WhenEventExists()
    {
        // Arrange
        var existing = _fixture.Events[0];
        var newStatus = EventStatus.Cancelled;

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(existing.Id))
            .ReturnsAsync(existing);
        _fixture.MockRepo.Setup(r => r.UpdateStatusAsync(existing.Id, newStatus))
            .ReturnsAsync(true);

        // Act
        var result = await _fixture.Service.UpdateStatusAsync(existing.Id, newStatus);

        // Assert
        Assert.True(result);
        _fixture.MockRepo.Verify(r => r.UpdateStatusAsync(existing.Id, newStatus), Times.Once);
    }

    [Trait("Method", "UpdateStatus")]
    [Fact]
    public async Task UpdateStatusAsync_ShouldThrowNotFoundException_WhenEventDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        var newStatus = EventStatus.Completed;

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Event?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.UpdateStatusAsync(missingId, newStatus));

        Assert.Equal("NotFound", ex.Error);
        _fixture.MockRepo.Verify(r => r.UpdateStatusAsync(It.IsAny<Guid>(), It.IsAny<EventStatus>()), Times.Never);
    }
}
