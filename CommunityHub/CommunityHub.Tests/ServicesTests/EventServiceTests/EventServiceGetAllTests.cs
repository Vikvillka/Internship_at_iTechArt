using CommunityHub.Domain.Enums;
using CommunityHub.Tests.Fixtures;
using Moq;
using System.Reflection;

namespace CommunityHub.Tests.ServicesTests.EventServiceTests;

public class EventServiceGetAllTests : IClassFixture<EventServiceTestFixture>
{
    private readonly EventServiceTestFixture _fixture;

    public EventServiceGetAllTests(EventServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "GetAllPlanned")]
    [Fact]
    public async Task GetAllPlannedAsync_ShouldReturnAllPlannedEvents()
    {
        // Arrange
        _fixture.MockRepo.Setup(r => r.GetAllPlannedAsync())
            .ReturnsAsync(_fixture.Events);

        // Act 
        var result = await _fixture.Service.GetAllPlannedAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.All(result, e => Assert.Equal(EventStatus.Planned, e.Status));
        _fixture.MockRepo.Verify(r => r.GetAllPlannedAsync(), Times.Once);
    }

    [Trait("Method", "GetAllPlanned")]
    [Fact]
    public async Task GetAllPlannedAsync_ShouldReturnEmptyList_WhenNoEventsExist()
    {
        // Arrange
        _fixture.MockRepo.Setup(r => r.GetAllPlannedAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _fixture.Service.GetAllPlannedAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _fixture.MockRepo.Verify(r => r.GetAllPlannedAsync(), Times.Once);
    }
}
