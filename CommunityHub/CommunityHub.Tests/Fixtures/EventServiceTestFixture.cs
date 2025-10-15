using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Application.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using Moq;

namespace CommunityHub.Tests.Fixtures;

public class EventServiceTestFixture
{
    public EventService Service { get; }
    public Mock<IEventRepository> MockRepo { get; }
    public Mock<ITagService> MockTagService { get; }
    public List<Event> Events { get; }

    public EventServiceTestFixture()
    {
        MockRepo = new Mock<IEventRepository>();
        MockTagService = new Mock<ITagService>();

        Service = new EventService(MockRepo.Object, MockTagService.Object);

        Events =
        [
            new()
            {
                Id = Guid.NewGuid(),
                Title = "TitleA",
                CommunityId = Guid.NewGuid(),
                EventDate = DateTime.Now,
                Status = EventStatus.Planned
            },
            new()
            {
                Id = Guid.NewGuid(),
                Title = "TitleB",
                CommunityId = Guid.NewGuid(),
                EventDate = DateTime.Now.AddDays(2),
                Status = EventStatus.Planned
            }
        ];
    }
}

