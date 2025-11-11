using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using UserService.Application.Intarfaces.Services;
using UserService.Contracts.DTOs.AuthDTOs;
using UserService.Domain.Entities;
using UserService.Domain.Exceptions;

namespace UserService.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly ILogger<JwtService> _logger;
    private readonly TimeSpan _accessTokenLifetime;
    private readonly TimeSpan _refreshTokenLifetime;

    public JwtService(IConfiguration config, ILogger<JwtService> logger)
    {
        _config = config;
        _logger = logger;

        _accessTokenLifetime = TimeSpan.FromMinutes(int.Parse(_config["Jwt:AccessTokenLifetimeMinutes"]!));
        _refreshTokenLifetime = TimeSpan.FromHours(int.Parse(_config["Jwt:AccessTokenLifetimeMinutes"]!));
    }

    public TokenResponse GenerateTokens(User user)
    {
        try
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username)
        };

            var accessToken = GenerateToken(claims, _accessTokenLifetime);
            var refreshToken = GenerateToken(claims, _refreshTokenLifetime);

            _logger.LogInformation("Generated tokens for user {Username}", user.Username);

            return new TokenResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error generating tokens for user {UserId}", user.Id);
            throw;
        }
    }

    public TokenResponse Refresh(string refreshToken)
    {
        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]!);

            tokenHandler.ValidateToken(refreshToken, new TokenValidationParameters
            {
                ValidIssuer = _config["Jwt:Issuer"],
                ValidAudience = _config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Subject;
            var username = jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;

            var newClaims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, username)
            };

            var newAccessToken = GenerateToken(newClaims, _accessTokenLifetime);
            var newRefreshToken = GenerateToken(newClaims, _refreshTokenLifetime);

            _logger.LogInformation("Refreshed access token for user {Username}", username);

            return new TokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        catch(Exception e)
        {
            _logger.LogError(e, "Error refreshing token");
            throw new UnauthorizedException("Unauthorized", "Invalid or expired refresh token");
        }
    }

    private string GenerateToken(IEnumerable<Claim> claims, TimeSpan lifetime)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(lifetime),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
