using System.IdentityModel.Tokens.Jwt;

using CommunityHub.Domain.Exceptions;
using CommunityHub.Tests.Fixtures;

namespace CommunityHub.Tests.ServicesTests.JwtServiceTests;

public class JwtServiceRefreshTokenTests : IClassFixture<JwtServiceTestFixture>
{
    private readonly JwtServiceTestFixture _fixture;

    public JwtServiceRefreshTokenTests(JwtServiceTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Trait("Method", "RefreshToken")]
    [Fact]
    public void Refresh_ShouldReturnNewAccessToken_WhenValid()
    {
        // Arrange
        var refreshToken = _fixture.Service.GenerateTokens(_fixture.TestUser).RefreshToken;

        // Act
        var newAccessToken = _fixture.Service.Refresh(refreshToken);

        // Assert
        Assert.False(string.IsNullOrWhiteSpace(newAccessToken));

        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(newAccessToken);

        Assert.Equal(_fixture.ConfigData["Jwt:Issuer"], jwt.Issuer);
        Assert.Contains(_fixture.ConfigData["Jwt:Audience"], jwt.Audiences);
        Assert.Equal(_fixture.TestUser.Id.ToString(), jwt.Subject);
    }

    [Trait("Method", "RefreshToken")]
    [Fact]
    public void Refresh_ShouldThrowUnauthorizedException_WhenInvalidToken()
    {
        // Arrange
        var invalidToken = "invalidToken";

        // Act & Assert
        var ex = Assert.Throws<UnauthorizedException>(() =>
            _fixture.Service.Refresh(invalidToken));

        Assert.Equal("Unauthorized", ex.Error);
    }
}
