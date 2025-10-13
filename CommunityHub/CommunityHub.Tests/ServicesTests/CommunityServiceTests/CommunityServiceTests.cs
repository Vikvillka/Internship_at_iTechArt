using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using Moq;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceTests
{
    private readonly CommunityService _service;
    private readonly Mock<ICommunityRepository> _mockRepo;
    private readonly List<Community> _communities;

    public CommunityServiceTests()
    {
        _mockRepo = new Mock<ICommunityRepository>();
        _service = new CommunityService(_mockRepo.Object);

        _communities = new List<Community>
        {
            new() { Id = Guid.NewGuid(), Name = "First" },
            new() { Id = Guid.NewGuid(), Name = "Second" }
        };
    }

    #region GetAllAsync Tests
    [Trait("Method", "GetAll")]
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCommunities()
    {
        // Arrange
        _mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(_communities);
        
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
        _mockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(new List<Community>());

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _mockRepo.Verify(r => r.GetAllAsync(), Times.Once);
    }
    #endregion

    #region GetByIdAsync Tests
    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCommunityById_WhenExists()
    {
        // Arrange
        var targetId = _communities[0].Id;
        var expectedCommunity = _communities[0];

        _mockRepo.Setup(r => r.GetByIdAsync(targetId))
            .ReturnsAsync(expectedCommunity);

        // Act
        var result = await _service.GetByIdAsync(targetId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(targetId, result.Id);
        _mockRepo.Verify(r => r.GetByIdAsync(targetId), Times.Once);
    }

    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _mockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var exeption = await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.GetByIdAsync(missingId)
        );

        Assert.Equal("NotFound", exeption.Error);
        _mockRepo.Verify(r => r.GetByIdAsync(missingId), Times.Once);
    }
    #endregion

    #region GetByIdAsync Tests
    [Trait("Method", "Create")]
    [Theory]
    [InlineData("Community", "Description", "Category", "City", "Country")]
    public async Task CreateAsync_ShouldReturnCreatedCommunity_WhenValidInput(
        string name,
        string description,
        string category,
        string city,
        string country)
    {
        // Arrange
        var newCommunity = new Community
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Category = category,
            City = city,
            Country = country
        };

        _mockRepo.Setup(r => r.CreateAsync(It.Is<Community>(c =>
            c.Name == name &&
            c.Description == description &&
            c.Category == category &&
            c.City == city &&
            c.Country == country)))
            .ReturnsAsync((Community c) => c);

        // Act
        var result = await _service.CreateAsync(newCommunity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(description, result.Description);
        Assert.Equal(category, result.Category);
        Assert.Equal(city, result.City);
        Assert.Equal(country, result.Country);
        _mockRepo.Verify(r => r.CreateAsync(It.IsAny<Community>()), Times.Once);
    }

    [Trait("Method", "Create")]
    [Theory]
    [InlineData(null)]
    public async Task CreateAsync_ShouldThrowConflictException_WhenNameExistsInSameCityAndCountry(string? name)
    {

    }

    #endregion
}
