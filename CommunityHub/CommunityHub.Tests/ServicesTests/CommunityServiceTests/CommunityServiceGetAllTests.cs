using Moq;

using CommunityHub.Tests.Fixtures;

namespace CommunityHub.Tests.ServicesTests.CommunityServiceTests;

public class CommunityServiceGetAllTests : IClassFixture<CommunityServiceTestFixture>
{
    private readonly CommunityServiceTestFixture _fixture;

    public CommunityServiceGetAllTests(CommunityServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

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
        _fixture.MockRepo.Verify(r => r.GetAllAsync(), Times.Once);
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
        _fixture.MockRepo.Verify(r => r.GetAllAsync(), Times.Once);
    }
}
