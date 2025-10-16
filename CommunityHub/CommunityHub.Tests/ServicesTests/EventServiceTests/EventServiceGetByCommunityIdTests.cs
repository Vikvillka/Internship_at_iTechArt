using Moq;

using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;

namespace CommunityHub.Tests.ServicesTests.EventServiceTests;

public class EventServiceGetByCommunityIdTests : IClassFixture<EventServiceTestFixture>
{
    private readonly EventServiceTestFixture _fixture;

    public EventServiceGetByCommunityIdTests(EventServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "GetByCommunityId")]
    [Fact]
    public async Task GetByCommunityIdAsync_ShouldReturnEvents_WhenCommunityExists()
    {
        // Arrange
        var communityId = _fixture.Events[0].CommunityId;
        var expectedEvents = _fixture.Events.Where(e => e.CommunityId == communityId).ToList();

        _fixture.MockCommunityService.Setup(s => s.GetByIdAsync(communityId))
            .ReturnsAsync(new Community { Id = communityId });

        _fixture.MockRepo.Setup(r => r.GetByCommunityIdAsync(communityId))
            .ReturnsAsync(expectedEvents);

        // Act
        var result = await _fixture.Service.GetByCommunityIdAsync(communityId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedEvents.Count, result.Count);
        Assert.All(result, e => Assert.Equal(communityId, e.CommunityId));
        _fixture.MockRepo.Verify(r => r.GetByCommunityIdAsync(communityId), Times.Once);
    }

    [Trait("Method", "GetByCommunityId")]
    [Fact]
    public async Task GetByCommunityIdAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingCommunityId = Guid.NewGuid();

        _fixture.MockCommunityService.Setup(s => s.GetByIdAsync(missingCommunityId))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.GetByCommunityIdAsync(missingCommunityId)
        );

        Assert.Equal("NotFound", exception.Error);
        _fixture.MockRepo.Verify(r => r.GetByCommunityIdAsync(missingCommunityId), Times.Never);
    }
}
