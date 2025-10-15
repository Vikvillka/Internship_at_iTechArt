using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;
using System.Reflection;

namespace CommunityHub.Tests.ServicesTests.EventServiceTests;

public class EventServiceCreateTests : IClassFixture<EventServiceTestFixture>
{
    private readonly EventServiceTestFixture _fixture;

    public EventServiceCreateTests(EventServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "Create")]
    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedEvent_WhenTitleAndTimeAreUnique()
    {
        // Arrange
        var newEvent = new Event
        {
            Id = Guid.NewGuid(),
            Title = "NewEvent",
            CommunityId = Guid.NewGuid(),
            EventDate = DateTime.Now.AddDays(5),
            Status = EventStatus.Planned
        };

        var tagIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        var mockTags = new List<EventTag>
        {
            new() { Id = tagIds[0], Name = "Tag1" },
            new() { Id = tagIds[1], Name = "Tag2" }
        };

        _fixture.MockRepo
            .Setup(r => r.ExistsWithSameTitleAndTimeAsync(
                newEvent.CommunityId, newEvent.Title, newEvent.EventDate))
            .ReturnsAsync(false);

        _fixture.MockTagService
            .Setup(s => s.GetByIdsAsync(tagIds))
            .ReturnsAsync(mockTags);

        _fixture.MockRepo
            .Setup(r => r.CreateAsync(It.Is<Event>(e =>
                e.Title == newEvent.Title &&
                e.EventDate == newEvent.EventDate &&
                e.CommunityId == newEvent.CommunityId)))
            .ReturnsAsync((Event e) => e);

        // Act
        var result = await _fixture.Service.CreateAsync(newEvent, tagIds);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newEvent.Title, result.Title);
        Assert.Equal(newEvent.EventDate, result.EventDate);
        Assert.Equal(mockTags.Count, result.Tags.Count);
        _fixture.MockRepo.Verify(r => r.CreateAsync(newEvent), Times.Once);
    }

    [Trait("Method", "Create")]
    [Fact]
    public async Task CreateAsync_ShouldThrowConflictException_WhenEventWithSameTitleAndTimeExists()
    {
        // Arrange
        var conflictingEvent = new Event
        {
            Id = Guid.NewGuid(),
            Title = _fixture.Events[0].Title,
            CommunityId = _fixture.Events[0].CommunityId,
            EventDate = _fixture.Events[0].EventDate
        };

        _fixture.MockRepo
            .Setup(r => r.ExistsWithSameTitleAndTimeAsync(
                conflictingEvent.CommunityId,
                conflictingEvent.Title,
                conflictingEvent.EventDate))
            .ReturnsAsync(true);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ConflictException>(() =>
            _fixture.Service.CreateAsync(conflictingEvent, [])
        );

        Assert.Equal("Conflict", exception.Error);
        _fixture.MockRepo.Verify(r => r.CreateAsync(It.IsAny<Event>()), Times.Never);
    }
}
