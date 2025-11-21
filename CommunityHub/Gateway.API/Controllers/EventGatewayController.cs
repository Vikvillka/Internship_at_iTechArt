using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.Contracts.DTOs.Enums;
using CommunityHub.Contracts.DTOs.EventDTOs;
using Gateway.API.Clients;
using Gateway.API.DTOs.EventDTOs;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/event")]
public class EventGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;
    private readonly IMapper _mapper;

    public EventGatewayController(IBestApiClient apiClient, IMapper mapper)
    {
        _apiClient = apiClient;
        _mapper = mapper;
    }

    [HttpGet("getAll")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<GatewayEventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllPlannedEventsAsync();
        var gatewayResponse = _mapper.Map<List<GatewayEventResponse>>(result);
        return Ok(gatewayResponse);
    }

    [HttpGet("get/{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(GatewayEventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _apiClient.GetEventByIdAsync(id);
        var gatewayResponse = _mapper.Map<GatewayEventResponse>(result);
        return Ok(gatewayResponse);
    }

    [HttpGet("getAllByCommunity/{communityId:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<GatewayEventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByCommunity(Guid communityId)
    {
        var result = await _apiClient.GetEventsByCommunityIdAsync(communityId);
        var gatewayResponse = _mapper.Map<List<GatewayEventResponse>>(result);
        return Ok(gatewayResponse);
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(GatewayEventResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(GatewayCreateEventRequest request)
    {
        var apiRequest = _mapper.Map<CreateEventRequest>(request);
        var result = await _apiClient.CreateEventAsync(apiRequest);
        var gatewayResponse = _mapper.Map<GatewayEventResponse>(result);
        return Ok(gatewayResponse);
    }

    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(GatewayUpdateEventRequest request)
    {
        var apiRequest = _mapper.Map<UpdateEventRequest>(request);
        await _apiClient.UpdateEventAsync(apiRequest);
        return Ok();
    }

    [HttpPatch("{id:guid}/status")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(Guid id, EventStatusDto newStatus)
    {
        await _apiClient.UpdateEventStatusAsync(id, newStatus);
        return Ok();
    }
}
