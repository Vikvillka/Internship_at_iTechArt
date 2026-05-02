using AutoMapper;
using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using Gateway.API.Clients;
using Gateway.API.DTOs.Common;
using Gateway.API.DTOs.CommunitiesDTOs;
using Gateway.API.DTOs.UserDTOs;
using Gateway.API.Interfaces;
using Gateway.API.Interfaces.Cache;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.GRpc;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/community")]
public class CommunityGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;
    private readonly IMapper _mapper;
    private readonly IMemoryCacheApiService _memoryCache;
    private readonly IUserGrpcClient _apiUserClient;

    public CommunityGatewayController(IBestApiClient apiClient, IMapper mapper, IMemoryCacheApiService memoryCache, IUserGrpcClient apiUserClient)
    {
        _apiClient = apiClient;
        _mapper = mapper;
        _memoryCache = memoryCache;
        _apiUserClient = apiUserClient;
    }

    [HttpGet("getAll")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<GatewayCommunityResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllCommunitiesAsync();
        var gatewayResponse = _mapper.Map<List<GatewayCommunityResponse>>(result);
        return Ok(gatewayResponse);
    }

    [HttpGet("get/{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayCommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _memoryCache.GetCommunityByIdAsync(id);
        var gatewayResponse = _mapper.Map<GatewayCommunityResponse>(result);
        return Ok(gatewayResponse);
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(GatewayCommunityResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(GatewayCreateCommunityRequest request)
    {
        var apiRequest = _mapper.Map<CreateCommunityRequest>(request);
        var existingUser = await _apiUserClient.GetUserByIdAsync(
                new GetUserByIdRequest { UserId = apiRequest.OwnerId.ToString() });
        _mapper.Map<GatewayUserResponse>(existingUser);

        var result = await _apiClient.CreateCommunityAsync(apiRequest);
        var gatewayResponse = _mapper.Map<GatewayCommunityResponse>(result);
        return Ok(gatewayResponse);
    }

    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(GatewayUpdateCommunityRequest request)
    {
        var apiRequest = _mapper.Map<UpdateCommunityRequest>(request);
        await _apiClient.UpdateCommunityAsync(apiRequest);
        return Ok();
    }

    [HttpDelete("delete/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _apiClient.DeleteCommunityByIdAsync(id);
        return NoContent();
    }

    [HttpPost("search")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(PagedResponse<GatewayCommunityResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetBySearch([FromBody] GatewayCommunitySearchRequest request)
    {
        var apiRequest = _mapper.Map<CommunitySearchRequest>(request);
        var result = await _apiClient.GetCommunitiesBySearchAsync(apiRequest);
        var gatewayResponse = _mapper.Map<PagedResponse<GatewayCommunityResponse>>(result);
        return Ok(gatewayResponse);
    }

    [HttpGet("getByUser/{id}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(List<GatewayCommunityResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCommnitiesByUserId(Guid id)
    {
        var result = await _apiClient.GetCommunityByUserIdAsync(id);
        var gatewayResponse = _mapper.Map<List<GatewayCommunityResponse>>(result);
        return Ok(gatewayResponse);
    }
}
