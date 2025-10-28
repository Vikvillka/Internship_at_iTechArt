using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Gateway.API.Clients;
using Gateway.API.DTOs.AuthDTOs;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/auth")]
public class AuthGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;

    public AuthGatewayController(IBestApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpPost("getTokens")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTokens(GatewayAuthRequest request)
    {
        var result = await _apiClient.GetTokensAsync(request);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(GatewayTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(GatewayRefreshRequest request)
    {
        var result = await _apiClient.RefreshTokenAsync(request);
        return Ok(result);
    }
}