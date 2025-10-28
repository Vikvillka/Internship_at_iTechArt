using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using Gateway.API.Clients;
using Gateway.API.DTOs.CommunitiesDTOs;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/community")]
public class CommunityGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;
    private readonly IMapper _mapper;

    public CommunityGatewayController(IBestApiClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
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
        var result = await _apiClient.GetCommunityByIdAsync(id);
        var gatewayResponse = _mapper.Map<GatewayCommunityResponse>(result);
        return Ok(gatewayResponse);
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(GatewayCommunityResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(GatewayCreateCommunityRequest request)
    {
        var apiRequest = _mapper.Map<CreateCommunityRequest>(request);
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
}
