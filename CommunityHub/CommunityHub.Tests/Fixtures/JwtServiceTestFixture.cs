using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;

using CommunityHub.Domain.Entities;
using CommunityHub.Infrastructure.Services;

namespace CommunityHub.Tests.Fixtures;

public class JwtServiceTestFixture
{
    public JwtService Service { get; }
    public User TestUser { get; }
    public Dictionary<string, string?> ConfigData;

    public JwtServiceTestFixture()
    {
        ConfigData = new()
        {
            ["Jwt:Key"] = "ThisIsASecretKeyForJwtTesting732173721!!",
            ["Jwt:Issuer"] = "TestHub",
            ["Jwt:Audience"] = "TestUsers"
        };

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(ConfigData)
            .Build();

        Service = new JwtService(configuration, NullLogger<JwtService>.Instance);

        TestUser = new()
        {
            Id = Guid.NewGuid(),
            Username = "TestUser",
            PasswordHash = "TestPassword"
        };
    }
}
