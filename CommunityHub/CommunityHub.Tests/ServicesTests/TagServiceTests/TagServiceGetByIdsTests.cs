using CommunityHub.Tests.Fixtures;
using Moq;

namespace CommunityHub.Tests.ServicesTests.TagServiceTests;

public class TagServiceGetByIdsTests : IClassFixture<TagServiceTestFixture>
{
    private readonly TagServiceTestFixture _fixture;

    public TagServiceGetByIdsTests(TagServiceTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Trait("Method", "GetByIds")]
    [Fact]
    public async Task GetByIdsAsync_ShouldReturnTags_WhenTagsExist()
    {
        // Arrange
        var ids = _fixture.Tags.Select(t => t.Id).ToList();
        _fixture.MockRepo.Setup(r => r.GetByIdsAsync(ids))
            .ReturnsAsync(_fixture.Tags);

        // Act
        var result = await _fixture.Service.GetByIdsAsync(ids);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(ids.Count, result.Count);
        _fixture.MockRepo.Verify(r => r.GetByIdsAsync(ids), Times.Once);
    }

    [Trait("Method", "GetByIds")]
    [Fact]
    public async Task GetByIdsAsync_ShouldReturnEmptyList_WhenNoTagsFound()
    {
        // Arrange
        var ids = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };
        _fixture.MockRepo.Setup(r => r.GetByIdsAsync(ids))
            .ReturnsAsync([]);

        // Act
        var result = await _fixture.Service.GetByIdsAsync(ids);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);
        _fixture.MockRepo.Verify(r => r.GetByIdsAsync(ids), Times.Once);
    }
}
