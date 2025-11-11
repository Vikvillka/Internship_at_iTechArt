using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Intarfaces.Services;
using UserService.Contracts.DTOs.AuthDTOs;

namespace UserService.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Basic")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;

    public AuthController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [HttpPost("getTokens")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetTokens([FromBody] AuthRequest request)
    {
        var user = await _userService.AuthenticateAsync(request.Username, request.Password);
        var tokens = _jwtService.GenerateTokens(user);
        return Ok(tokens);
    }

    [HttpPost("refresh")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult Refresh([FromBody] RefreshRequest request)
    {
        var newAccessToken = _jwtService.Refresh(request.RefreshToken);
        return Ok(newAccessToken);
    }

    [HttpPost("validateBasic")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ValidateBasic(AuthRequest request)
    {
        var user = await _userService.AuthenticateAsync(request.Username, request.Password);
        return Ok();
    }
}
