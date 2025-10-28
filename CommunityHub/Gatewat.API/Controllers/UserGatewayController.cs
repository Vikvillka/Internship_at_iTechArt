using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using CommunityHub.Contracts.DTOs.UserDTOs;
using Gateway.API.Clients;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/user")]
public class UserGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;

    public UserGatewayController(IBestApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(GatewayCreateUserRequest request)
    {
        var result = await _apiClient.RegisterUserAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    [Authorize(AuthenticationSchemes = "Basic")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _apiClient.DeleteUserByIdAsync(id);
        return NoContent();
    }
}
