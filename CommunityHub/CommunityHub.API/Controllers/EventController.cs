using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.API.DTOs.EventDTOs;
using CommunityHub.API.Extensions.Mappings;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _repository;
    private readonly IMapper _mapper;

    public EventController(IEventRepository repository, IMapper mapper, ITagRepository tagRepository)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllPlanned()
    {
        var events = await _repository.GetAllPlannedAsync();
        var response = events.Select(e => e.FromEntity()).ToList();
        return Ok(response);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var eventEnity = await _repository.GetByIdAsync(id);
        if (eventEnity == null) return NotFound();

        var response = eventEnity.FromEntity();
        return Ok(response);
    }

    [HttpGet("getAllByCommunity/{communityId}")]
    [ProducesResponseType(typeof(List<EventResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllForCommunity(Guid communityId)
    {
        var events = await _repository.GetByCommunityIdAsync(communityId);
        var response = events.Select(e => e.FromEntity()).ToList();
        return Ok(response);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateEventRequest request)
    {
        var eventEntity = _mapper.Map<Event>(request);
        var created = await _repository.CreateWithTagsAsync(eventEntity, request.TagIds);
        var response = created.FromEntity();
        return Ok(response);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(EventResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateEventRequest request)
    {
        var entityUpdate = _mapper.Map<Event>(request);
        var success = await _repository.UpdateAsync(entityUpdate);

        if (!success) return BadRequest();
        return Ok();
    }

    [HttpPatch("{id}/status")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateStatus([FromBody] EventStatus newStatus, Guid id)
    {
        var success = await _repository.UpdateStatusAsync(id, newStatus);
        if (!success) return NotFound();

        return Ok();
    }
}

