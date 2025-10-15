using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Services;
using CommunityHub.Domain.Entities;
using Moq;

namespace CommunityHub.Tests.Fixtures
{
    public class TagServiceTestFixture
    {
        public TagService Service { get; }
        public Mock<ITagRepository> MockRepo { get; }
        public List<EventTag> Tags { get; }

        public TagServiceTestFixture()
        {
            MockRepo = new Mock<ITagRepository>();
            Service = new TagService(MockRepo.Object);

            Tags =
            [
                new() { Id = Guid.NewGuid(), Name = "TagA" },
                new() { Id = Guid.NewGuid(), Name = "TagB" }
            ];
        }
    }
}
