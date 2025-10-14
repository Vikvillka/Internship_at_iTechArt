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
            new() { Id = Guid.NewGuid(), Name = "First", City = "CityA", Country = "CountryA" },
            new() { Id = Guid.NewGuid(), Name = "Second", City = "CityB", Country = "CountryB" }
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

    #region CreateAsync Tests
    [Trait("Method", "Create")]
    [Theory]
    [InlineData("Community", "Description", "Category", "CityC", "CountryC")]
    public async Task CreateAsync_ShouldReturnCreatedCommunity_WhenValidInputAndNameIsUnique(
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

        _mockRepo.Setup(r => r.SearchAsync(null, city, country))
            .ReturnsAsync([]);

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
    [InlineData("First", "CityA", "CountryA")]
    public async Task CreateAsync_ShouldThrowConflictException_WhenNameIsNotUnique(
        string name, 
        string city, 
        string country)
    {
        // Arrange
        var duplicateCommunity = new Community
        {
            Id = Guid.NewGuid(),
            Name = name,
            City = city,
            Country = country
        };

        _mockRepo.Setup(r => r.SearchAsync(null, city, country))
            .ReturnsAsync(_communities.Where(c =>
                c.City.Equals(city, StringComparison.OrdinalIgnoreCase) &&
                c.Country.Equals(country, StringComparison.OrdinalIgnoreCase)
            ).ToList());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ConflictException>(() =>
            _service.CreateAsync(duplicateCommunity)
        );

        Assert.Equal("Conflict", exception.Error);
        _mockRepo.Verify(r => r.SearchAsync(null, city, country), Times.Once);
    }
    #endregion

    #region UpdateAsync Tests
    [Trait("Method", "Update")]
    [Theory]
    [InlineData("Community", "Description", "Category", "CityC", "CountryC")]
    public async Task UpdateAsync_ShouldReturnTrue_WhenCommunityExistsAndNameIsUnique(
        string name, 
        string description, 
        string category, 
        string city, 
        string country)
    {
        // Arrange
        var existing = _communities[0];
        var updatedCommunity = new Community
        {
            Id = existing.Id,
            Name = name,
            Description = description,
            Category = category,
            City = city,
            Country = country
        };

        _mockRepo.Setup(r => r.GetByIdAsync(existing.Id))
            .ReturnsAsync(existing);

        _mockRepo.Setup(r => r.SearchAsync(null, city, country))
            .ReturnsAsync(_communities.Where(c => c.Id != existing.Id).ToList());

        _mockRepo.Setup(r => r.UpdateAsync(updatedCommunity))
            .ReturnsAsync(true);

        // Act
        var result = await _service.UpdateAsync(updatedCommunity);

        // Assert
        Assert.True(result);
        _mockRepo.Verify(r => r.UpdateAsync(updatedCommunity), Times.Once);
    }

    [Trait("Method", "Update")]
    [Fact]
    public async Task UpdateAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingCommunity = new Community
        {
            Id = Guid.NewGuid(),
            Name = "Name",
            City = "CityA",
            Country = "CountryA"
        };

        _mockRepo.Setup(r => r.GetByIdAsync(missingCommunity.Id))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.UpdateAsync(missingCommunity));

        Assert.Equal("NotFound", ex.Error);
        _mockRepo.Verify(r => r.UpdateAsync(It.IsAny<Community>()), Times.Never);
    }
    #endregion

    #region DeleteAsync Tests
    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenCommunityExists()
    {
        // Arrange
        var existing = _communities[0];

        _mockRepo.Setup(r => r.GetByIdAsync(existing.Id))
            .ReturnsAsync(existing);
        _mockRepo.Setup(r => r.DeleteAsync(existing.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _service.DeleteAsync(existing.Id);

        // Assert
        Assert.True(result);
        _mockRepo.Verify(r => r.DeleteAsync(existing.Id), Times.Once);
    }

    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _mockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _service.DeleteAsync(missingId));

        Assert.Equal("NotFound", ex.Error);
        _mockRepo.Verify(r => r.DeleteAsync(It.IsAny<Guid>()), Times.Never);
    }
    #endregion
}
