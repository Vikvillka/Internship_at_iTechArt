using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.TagServiceTests;

public class TagServiceGetAllTests : IClassFixture<TagServiceTestFixture>
{
    private readonly TagServiceTestFixture _fixture;

    public TagServiceGetAllTests(TagServiceTestFixture fixture)
    {
        _fixture = fixture;
        _fixture.MockRepo.Invocations.Clear();
    }

    [Trait("Method", "GetAll")]
    [Fact]
    public async Task GetAllAsync_ShouldReturnAllTags_WhenTagsExist()
    {
        // Arrange
        _fixture.MockRepo.Setup(r => r.GetAllAsync())
            .ReturnsAsync(_fixture.Tags);

        // Act
        var result = await _fixture.Service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_fixture.Tags.Count, result.Count);
        _fixture.MockRepo.Verify(r => r.GetAllAsync(), Times.Once);
    }

    [Trait("Method", "GetAll")]
    [Fact]
    public async Task GetAllAsync_ShouldReturnEmptyList_WhenNoTagsExist()
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
