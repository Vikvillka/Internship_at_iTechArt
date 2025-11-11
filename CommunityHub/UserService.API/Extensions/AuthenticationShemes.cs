using Microsoft.AspNetCore.Authentication;

using UserService.API.Authentication;

namespace UserService.API.Extensions;

public static class AuthenticationShemes
{
    public static void AddAuthenticationSchemes(this IServiceCollection services, IConfiguration config)
    {
        services.AddAuthentication("Basic")
            .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("Basic", null);
    }
}
