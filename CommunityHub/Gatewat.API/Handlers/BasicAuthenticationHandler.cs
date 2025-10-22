using CommunityHub.Contracts.DTOs.AuthDTOs;
using Gateway.API.Clients;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Gateway.API.Handlers;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IUserApiClient _userApiClient;
    private const string basicHeader = "Basic realm=\"Gateway\"";

    public BasicAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IUserApiClient userApiClient
    ) : base(options, logger, encoder, clock)
    {
        _userApiClient = userApiClient;
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

            var response = await _userApiClient.ValidateBasicAsync(new AuthRequest { Username = username, Password = password });
            if (!response.IsSuccessStatusCode)
            {
                Response.Headers["WWW-Authenticate"] = basicHeader;
                return AuthenticateResult.Fail("Invalid Username or Password");
            }

            var claims = new[] 
            { 
                new Claim(ClaimTypes.Name, username) 
            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error during Basic authentication");
            Response.Headers["WWW-Authenticate"] = basicHeader;
            return AuthenticateResult.Fail("Error during authentication");
        }
    }
}


