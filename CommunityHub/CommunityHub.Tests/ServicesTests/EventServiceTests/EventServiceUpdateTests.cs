using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.EventServiceTests;

public class EventServiceUpdateTests : IClassFixture<EventServiceTestFixture>
{
    private readonly EventServiceTestFixture _fixture;

    public EventServiceUpdateTests(EventServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "Update")]
    [Fact]
    public async Task UpdateAsync_ShouldReturnTrue_WhenEventExistsAndTitleAndTimeAreUnique()
    {
        // Arrange
        var existing = _fixture.Events[0];
        var updatedEvent = new Event
        {
            Id = existing.Id,
            Title = "UpdatedTitle",
            EventDate = DateTime.Now.AddDays(10),
            CommunityId = existing.CommunityId,
            Status = EventStatus.Planned
        };

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(existing.Id))
            .ReturnsAsync(existing);

        _fixture.MockRepo.Setup(r => r.ExistsWithSameTitleAndTimeAsync(
            updatedEvent.CommunityId,
            updatedEvent.Title,
            updatedEvent.EventDate))
            .ReturnsAsync(false);

        _fixture.MockRepo.Setup(r => r.UpdateAsync(updatedEvent))
            .ReturnsAsync(true);

        // Act
        var result = await _fixture.Service.UpdateAsync(updatedEvent);

        // Assert
        Assert.True(result);
        _fixture.MockRepo.Verify(r => r.UpdateAsync(updatedEvent), Times.Once);
    }

    [Trait("Method", "Update")]
    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenEventDoesNotExist()
    {
        // Arrange
        var missingEvent = new Event
        {
            Id = Guid.NewGuid(),
            Title = "Title",
            EventDate = DateTime.Now.AddDays(1),
            CommunityId = Guid.NewGuid()
        };

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingEvent.Id))
            .ReturnsAsync((Event?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.UpdateAsync(missingEvent));

        Assert.Equal("NotFound", ex.Error);
        _fixture.MockRepo.Verify(r => r.UpdateAsync(It.IsAny<Event>()), Times.Never);
    }
}
