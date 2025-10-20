using Gateway.API.Clients;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.Contracts.DTOs.Enums;
using CommunityHub.Contracts.DTOs.EventDTOs;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/event")]
public class EventGatewayController : ControllerBase
{
    private readonly IMyBestApi _apiClient;

    public EventGatewayController(IMyBestApi apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllPlannedEventsAsync();
        return Ok(result);
    }

    [HttpGet("get/{id:guid}")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _apiClient.GetEventByIdAsync(id);
        return Ok(result);
    }

    [HttpGet("getAllByCommunity/{communityId:guid}")]
    [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllByCommunity(Guid communityId)
    {
        var result = await _apiClient.GetEventsByCommunityIdAsync(communityId);
        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        var result = await _apiClient.CreateEventAsync(request);
        return Ok(result);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateEventRequest request)
    {
        var result = await _apiClient.UpdateEventAsync(request);
        return Ok(result);
    }

    [HttpPatch("{id:guid}/status")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] EventStatusDto newStatus)
    {
        var result = await _apiClient.UpdateEventStatusAsync(id, newStatus);
        return Ok(result);
    }
}
