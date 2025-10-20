using Gateway.API.Clients;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.Contracts.DTOs.AuthDTOs;

namespace Gateway.API.Controllers;

public class AuthGatewayController : ControllerBase
{
    private readonly IMyBestApi _apiClient;

    public AuthGatewayController(IMyBestApi apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpPost("getTokens")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTokens([FromBody] AuthRequest request)
    {
        var result = await _apiClient.GetTokensAsync(request);
        return Ok(result);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
    {
        var result = await _apiClient.RefreshTokenAsync(request);
        return Ok(result);
    }
}