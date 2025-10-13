using Moq;

using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Services;
using CommunityHub.Domain.Entities;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceTests
{
    private readonly CommunityService _service;
    private readonly Mock<ICommunityRepository> _mockRepo;

    public CommunityServiceTests()
    {
        _mockRepo = new Mock<ICommunityRepository>();
        _service = new CommunityService(_mockRepo.Object);
    }

    #region GetAllAsync Tests
    [Trait("Method", "GetAll")]
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCommunities()
    {
        // Arrange
        var communities = new List<Community>
        {
            new() {},
            new() {}
        };

        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(communities);
        
        // Act
        var result = await _service.GetAllAsync();
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Trait("Method", "GetAll")]
    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoCommunitiesExist()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Community>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
    }
    #endregion

    #region GetByIdAsync Tests
    public async Task GetByIdAsync_ShouldReturnCommunityById()
    {
        // Arrange
        var communities = new List<Community>
        {
            new() {Id = Guid.NewGuid()},
            new() {Id = Guid.NewGuid()}
        };

        _mockRepo.Setup(r => r.GetByIdAsync(communities.)).ReturnsAsync(communities);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
    }
    #endregion
}
