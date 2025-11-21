//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Logging.Abstractions;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Text;

//using CommunityHub.Infrastructure.Services;
//using CommunityHub.Tests.Fixtures;

//namespace CommunityHub.Tests.ServicesTests.JwtServiceTests;

//public class JwtServiceGenerateTokensTests : IClassFixture<JwtServiceTestFixture>
//{
//    private readonly JwtServiceTestFixture _fixture;

//    public JwtServiceGenerateTokensTests(JwtServiceTestFixture fixture)
//    {
//        _fixture = fixture;
//    }

//    [Trait("Method", "GenerateTokens")]
//    [Fact]
//    public void GenerateTokens_ShouldReturnValidTokens_WhenUserIsValid()
//    {
//        // Act
//        var result = _fixture.Service.GenerateTokens(_fixture.TestUser);

//        // Assert
//        Assert.NotNull(result);
//        Assert.False(string.IsNullOrWhiteSpace(result.AccessToken));
//        Assert.False(string.IsNullOrWhiteSpace(result.RefreshToken));
//        Assert.NotEqual(result.AccessToken, result.RefreshToken);

//        var handler = new JwtSecurityTokenHandler();
//        var key = Encoding.UTF8.GetBytes(_fixture.ConfigData["Jwt:Key"]!);
//        var parameters = new TokenValidationParameters
//        {
//            ValidIssuer = _fixture.ConfigData["Jwt:Issuer"],
//            ValidAudience = _fixture.ConfigData["Jwt:Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateIssuerSigningKey = true,
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = false
//        };

//        handler.ValidateToken(result.AccessToken, parameters, out var validatedToken);
//        var jwtToken = (JwtSecurityToken)validatedToken;

//        Assert.Equal(_fixture.TestUser.Id.ToString(), jwtToken.Subject);
//        Assert.Contains(jwtToken.Claims, c =>
//            c.Type == JwtRegisteredClaimNames.UniqueName && c.Value == _fixture.TestUser.Username);
//    }

//    [Trait("Method", "GenerateTokens")]
//    [Fact]
//    public void GenerateTokens_ShouldThrow_WhenConfigInvalid()
//    {
//        // Arrange
//        var invalidConfig = new ConfigurationBuilder()
//            .AddInMemoryCollection(new Dictionary<string, string?>
//            {
//                ["Jwt:Issuer"] = "FailHub",
//                ["Jwt:Audience"] = "FailUsers"
//            })
//            .Build();

//        var service = new JwtService(invalidConfig, NullLogger<JwtService>.Instance);
//        var user = _fixture.TestUser;

//        // Act & Assert
//        Assert.Throws<ArgumentNullException>(() => service.GenerateTokens(user));
//    }
//}