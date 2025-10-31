using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using CommunityHub.API.Authentication;

namespace CommunityHub.API.Extensions;

public static class AuthenticationShemes
{
    public static void AddAuthenticationSchemes(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication("Basic")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
    }
}
