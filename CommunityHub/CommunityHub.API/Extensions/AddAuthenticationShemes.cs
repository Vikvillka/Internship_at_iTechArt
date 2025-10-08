using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using CommunityHub.API.Authentication;

namespace CommunityHub.API.Extensions;

public static class AddAuthenticationShemes
{
    public static void AddAuthenticationSchemes(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
           options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null!)
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
    }
}
