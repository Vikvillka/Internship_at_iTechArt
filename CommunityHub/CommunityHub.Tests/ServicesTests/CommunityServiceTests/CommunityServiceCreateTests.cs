using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceCreateTests : IClassFixture<CommunityServiceTestFixture>
{
    private readonly CommunityServiceTestFixture _fixture;

    public CommunityServiceCreateTests(CommunityServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

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
        _fixture.MockRepo.Verify(r => r.CreateAsync(newCommunity), Times.Once);
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
        _fixture.MockRepo.Verify(r => r.CreateAsync(It.IsAny<Community>()), Times.Never);
    }
}
