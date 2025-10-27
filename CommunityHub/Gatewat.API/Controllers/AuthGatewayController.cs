using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

using CommunityHub.Contracts.DTOs.AuthDTOs;
using Gateway.API.Clients;

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
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTokens(AuthRequest request)
    {
        var result = await _apiClient.GetTokensAsync(request);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(RefreshRequest request)
    {
        var result = await _apiClient.RefreshTokenAsync(request);
        return Ok(result);
    }
}