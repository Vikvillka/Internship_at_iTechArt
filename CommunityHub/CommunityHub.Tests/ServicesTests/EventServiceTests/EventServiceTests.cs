using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Application.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using Moq;

namespace CommunityHub.Tests.ServicesTests.EventServiceTests;

public class EventServiceTests
{
    private readonly EventService _service;

    private readonly Mock<IEventRepository> _mockRepo;
    private readonly Mock<ITagService> _mockTagService;

    private readonly List<Event> _events;
    
    public EventServiceTests()
    {
        _mockRepo = new Mock<IEventRepository>();
        _mockTagService = new Mock<ITagService>();
        _service = new EventService(_mockRepo.Object, _mockTagService.Object);

        _events =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Title = "TitleA",
                CommunityId = Guid.NewGuid(),
                EventDate = DateTime.Now,
                Status = EventStatus.Planned
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "TitleB",
                CommunityId = Guid.NewGuid(),
                EventDate = DateTime.Now.AddDays(2),
                Status = EventStatus.Planned
            }
        ];
    }

    #region GetAllAsync Tests
    [Trait("Method", "GetAllPlanned")]
    [Fact]
    public async Task GetAllPlannedAsync_ShouldReturnAllPlannedEvents()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllPlannedAsync())
            .ReturnsAsync(_events);

        // Act 
        var result = await _service.GetAllPlannedAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, e => Assert.Equal(EventStatus.Planned, e.Status));
        _mockRepo.Verify(r => r.GetAllPlannedAsync(), Times.Once);
    }

    [Trait("Method", "GetAllPlanned")]
    [Fact]
    public async Task GetAllPlannedAsync_ShouldReturnEmptyList_WhenNoEventsExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllPlannedAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _service.GetAllPlannedAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepo.Verify(r => r.GetAllPlannedAsync(), Times.Once);
    }
    #endregion
}
