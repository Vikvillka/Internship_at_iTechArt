using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Gateway.API.Handlers;

namespace Gateway.API.Extensions;

public static class AuthenticationSchemes
{
    public static void AddAuthenticationSchemes(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = config["CommunityServiceApi:Jwt:Issuer"],
                ValidAudience = config["CommunityServiceApi:Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CommunityServiceApi:Jwt:Key"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };
        })
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
    }
}
