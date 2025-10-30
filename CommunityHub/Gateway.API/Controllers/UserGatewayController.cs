using AutoMapper;
using CommunityHub.Contracts.DTOs.UserDTOs;
using Gateway.API.Clients;
using Gateway.API.DTOs.UserDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/user")]
public class UserGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;
    private readonly IMapper _mapper;

    public UserGatewayController(IBestApiClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(GatewayCreateUserRequest request)
    {
        var apiRequest = _mapper.Map<CreateUserRequest>(request);
        var result = await _apiClient.RegisterUserAsync(apiRequest);
        var gatewayResponse = _mapper.Map<GatewayUserResponse>(result);
        return Ok(gatewayResponse);
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
