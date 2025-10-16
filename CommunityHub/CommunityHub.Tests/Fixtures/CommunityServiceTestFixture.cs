using Moq;

using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Services;
using CommunityHub.Domain.Entities;

namespace CommunityHub.Tests.Fixtures;

public class CommunityServiceTestFixture
{
    internal CommunityService Service { get; }
    internal Mock<ICommunityRepository> MockRepo { get; }
    internal List<Community> Communities { get; }

    public CommunityServiceTestFixture()
    {
        MockRepo = new Mock<ICommunityRepository>();
        Service = new CommunityService(MockRepo.Object);

        Communities =
        [
            new() {
                Id = Guid.NewGuid(),
                Name = "First",
                City = "CityA",
                Country = "CountryA"
            },
            new() {
                Id = Guid.NewGuid(),
                Name = "Second",
                City = "CityB",
                Country = "CountryB"
            }
        ];
    }
}
