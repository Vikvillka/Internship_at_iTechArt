using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Gateway.API.DTOs.AuthDTOs;
using Gateway.API.Interfaces;
using UserService.GRpc;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/auth")]
public class AuthGatewayController : ControllerBase
{
    private readonly IAuthGrpcClient _apiClient;
    private readonly IMapper _mapper;

    public AuthGatewayController(IAuthGrpcClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    [HttpPost("getTokens")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTokens(GatewayAuthRequest request)
    {
        var apiRequest = _mapper.Map<AuthRequest>(request);
        var result = await _apiClient.GetTokensAsync(apiRequest);
        var gatewayResponse = _mapper.Map<GatewayTokenResponse>(result);
        return Ok(gatewayResponse);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(GatewayTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh(GatewayRefreshRequest request)
    {
        var apiRequest = _mapper.Map<RefreshRequest>(request);
        var result = await _apiClient.RefreshTokenAsync(apiRequest);
        var gatewayResponse = _mapper.Map<GatewayTokenResponse>(result);
        return Ok(gatewayResponse);
    }
}