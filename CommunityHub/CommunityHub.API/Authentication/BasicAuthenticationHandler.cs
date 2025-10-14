using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

using CommunityHub.Application.Interfaces.Services;

namespace CommunityHub.API.Authentication;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserService _userService;
    private const string basicHeader = "Basic realm=\"CommunityHub\"";

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock,
        IUserService userService
        ) : base(options, logger, encoder, clock)
    {
        _userService = userService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            Response.Headers["WWW-Authenticate"] = basicHeader;
            return AuthenticateResult.Fail("Missing Authorization Header");
        }
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);

            if (authHeader.Scheme != "Basic")
            {
                Response.Headers["WWW-Authenticate"] = basicHeader;
                return AuthenticateResult.Fail("Invalid Authorization Scheme");
            }

            var credentialBytes = Convert.FromBase64String(authHeader.Parameter ?? "");
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':', 2);
            if (credentials.Length != 2)
            {
                Response.Headers["WWW-Authenticate"] = basicHeader;
                return AuthenticateResult.Fail("Invalid Authorization Header");
            }
            var username = credentials[0];
            var password = credentials[1];
            
            var user = await _userService.GetByUsernameAsync(username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                Response.Headers["WWW-Authenticate"] = basicHeader;
                return AuthenticateResult.Fail("Invalid Username or Password");
            }
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
           
            return AuthenticateResult.Success(ticket);
        }
        catch(Exception e)
        {
            Logger.LogError(e, "Error during authentication");
            Response.Headers["WWW-Authenticate"] = basicHeader;
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
}
