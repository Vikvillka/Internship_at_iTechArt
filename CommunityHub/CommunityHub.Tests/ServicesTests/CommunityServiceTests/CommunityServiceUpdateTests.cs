using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceUpdateTests : IClassFixture<CommunityServiceTestFixture>
{
    private readonly CommunityServiceTestFixture _fixture;

    public CommunityServiceUpdateTests(CommunityServiceTestFixture fixture)
    {
        _fixture = fixture;
    }

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
}
