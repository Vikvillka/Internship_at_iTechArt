using Microsoft.AspNetCore.Authentication;

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
