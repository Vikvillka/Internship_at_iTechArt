using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UserService.GRpc;
using Gateway.API.DTOs.UserDTOs;
using Gateway.API.Interfaces;
using Gateway.API.Interfaces.Cache;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/user")]
public class UserGatewayController : ControllerBase
{
    private readonly IUserGrpcClient _apiClient;
    private readonly IRedisCacheApiService _redisCacheApi;
    private readonly IMapper _mapper;

    public UserGatewayController(IUserGrpcClient apiClient, IMapper mapper, IRedisCacheApiService redisCacheApi)
    {
        _apiClient = apiClient;
        _mapper = mapper;
        _redisCacheApi = redisCacheApi;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(GatewayCreateUserRequest request)
    {
        var apiRequest = _mapper.Map<RegisterUserRequest>(request);
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
        var result = await _apiClient.DeleteUserAsync(new DeleteUserRequest { UserId = id.ToString() });
        var success = _mapper.Map<bool>(result);
        return success ? NoContent() : NotFound();
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _redisCacheApi.GetUserByIdAsync(id);
        var gatewayResponse = _mapper.Map<GatewayUserResponse>(result);
        return Ok(gatewayResponse);
    }

    [HttpGet("getAll")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<GatewayUserResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllUsersAsync(new Google.Protobuf.WellKnownTypes.Empty());
        var mapped = _mapper.Map<List<GatewayUserResponse>>(result);
        return Ok(mapped);
    }
}
