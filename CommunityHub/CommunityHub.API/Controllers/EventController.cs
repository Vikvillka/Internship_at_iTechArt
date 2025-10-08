using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.API.DTOs.EventDTOs;
using CommunityHub.API.Extensions.Mappings;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Enums;
using CommunityHub.Application.Interfaces.Services;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventService _service;
    private readonly IMapper _mapper;

    public EventController(IEventService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPlanned()
    {
        var events = await _service.GetAllPlannedAsync();
        var response = events.Select(e => e.FromEntity()).ToList();
        return Ok(response);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var eventEntity = await _service.GetByIdAsync(id);
        var response = eventEntity?.FromEntity();
        return Ok(response);
    }

    [HttpGet("getAllByCommunity/{communityId}")]
    [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllForCommunity(Guid communityId)
    {
        var events = await _service.GetByCommunityIdAsync(communityId);
        var response = events.Select(e => e.FromEntity()).ToList();
        return Ok(response);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        var eventEntity = _mapper.Map<Event>(request);
        var created = await _service.CreateAsync(eventEntity, request.TagIds);
        var response = created.FromEntity();
        return Ok(response);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateEventRequest request)
    {
        var entityUpdate = _mapper.Map<Event>(request);
        await _service.UpdateAsync(entityUpdate);
        return Ok();
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus([FromBody] EventStatus newStatus, Guid id)
    {
        await _service.UpdateStatusAsync(id, newStatus);
        return Ok();
    }
}

