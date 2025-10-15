using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceTests : IClassFixture<CommunityServiceTestFixture>
{
    private readonly CommunityServiceTestFixture _fixture;

    public CommunityServiceTests(CommunityServiceTestFixture fixture)
    {
        _fixture = fixture;
    }

    #region GetAllAsync Tests
    [Trait("Method", "GetAll")]
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllCommunities()
    {
        // Arrange
        _fixture.MockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(_fixture.Communities);
        
        // Act
        var result = await _fixture.Service.GetAllAsync();
        
        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

    [Trait("Method", "GetAll")]
    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoCommunitiesExist()
    {
        // Arrange
        _fixture.MockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync([]);

        // Act
        var result = await _fixture.Service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
    }
    #endregion

    #region GetByIdAsync Tests
    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldReturnCommunityById_WhenExists()
    {
        // Arrange
        var targetId = _fixture.Communities[0].Id;
        var expectedCommunity = _fixture.Communities[0];

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(targetId))
            .ReturnsAsync(expectedCommunity);

        // Act
        var result = await _fixture.Service.GetByIdAsync(targetId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(targetId, result.Id);
    }

    [Trait("Method", "GetById")]
    [Fact]
    public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var exeption = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.GetByIdAsync(missingId)
        );

        Assert.Equal("NotFound", exeption.Error);
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

        _fixture.MockRepo.Setup(r => r.SearchAsync(null, city, country))
            .ReturnsAsync([]);

        _fixture.MockRepo.Setup(r => r.CreateAsync(It.Is<Community>(c =>
            c.Name == name &&
            c.Description == description &&
            c.Category == category &&
            c.City == city &&
            c.Country == country)))
            .ReturnsAsync((Community c) => c);

        // Act
        var result = await _fixture.Service.CreateAsync(newCommunity);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(name, result.Name);
        Assert.Equal(description, result.Description);
        Assert.Equal(category, result.Category);
        Assert.Equal(city, result.City);
        Assert.Equal(country, result.Country);
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

        _fixture.MockRepo.Setup(r => r.SearchAsync(null, city, country))
            .ReturnsAsync(_fixture.Communities.Where(c =>
                c.City.Equals(city, StringComparison.OrdinalIgnoreCase) &&
                c.Country.Equals(country, StringComparison.OrdinalIgnoreCase)
            ).ToList());

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ConflictException>(() =>
            _fixture.Service.CreateAsync(duplicateCommunity)
        );

        Assert.Equal("Conflict", exception.Error);
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
        var existing = _fixture.Communities[0];
        var updatedCommunity = new Community
        {
            Id = existing.Id,
            Name = name,
            Description = description,
            Category = category,
            City = city,
            Country = country
        };

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(existing.Id))
            .ReturnsAsync(existing);

        _fixture.MockRepo.Setup(r => r.SearchAsync(null, city, country))
            .ReturnsAsync(_fixture.Communities.Where(c => c.Id != existing.Id).ToList());

        _fixture.MockRepo.Setup(r => r.UpdateAsync(updatedCommunity))
            .ReturnsAsync(true);

        // Act
        var result = await _fixture.Service.UpdateAsync(updatedCommunity);

        // Assert
        Assert.True(result);
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

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingCommunity.Id))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.UpdateAsync(missingCommunity));

        Assert.Equal("NotFound", ex.Error);
    }
    #endregion

    #region DeleteAsync Tests
    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldReturnTrue_WhenCommunityExists()
    {
        // Arrange
        var existing = _fixture.Communities[0];

        _fixture.MockRepo.Setup(r => r.GetByIdAsync(existing.Id))
            .ReturnsAsync(existing);
        _fixture.MockRepo.Setup(r => r.DeleteAsync(existing.Id))
            .ReturnsAsync(true);

        // Act
        var result = await _fixture.Service.DeleteAsync(existing.Id);

        // Assert
        Assert.True(result);
    }

    [Trait("Method", "Delete")]
    [Fact]
    public async Task DeleteAsync_ShouldThrowNotFoundException_WhenCommunityDoesNotExist()
    {
        // Arrange
        var missingId = Guid.NewGuid();
        _fixture.MockRepo.Setup(r => r.GetByIdAsync(missingId))
            .ReturnsAsync((Community?)null);

        // Act & Assert
        var ex = await Assert.ThrowsAsync<NotFoundException>(() =>
            _fixture.Service.DeleteAsync(missingId));

        Assert.Equal("NotFound", ex.Error);
    }
    #endregion
}
