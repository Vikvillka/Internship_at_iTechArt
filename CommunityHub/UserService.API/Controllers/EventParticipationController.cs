using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UserService.API.Extensions.Mappings;
using UserService.Application.Intarfaces.Services;
using UserService.Contracts.DTOs.ParticipationDTOs;
using UserService.Domain.Entities;

namespace UserService.API.Controllers;

[ApiController]
[Route("participation")]
[Authorize(AuthenticationSchemes = "Basic")]
public class EventParticipationController : ControllerBase
{
    private readonly IEventParticipationService _service;
    private readonly IMapper _mapper;

    public EventParticipationController(IEventParticipationService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("join")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Participate(CreateEventParticipationRequest request)
    {
        var participation = _mapper.Map<EventParticipation>(request);
        await _service.ParticipateAsync(participation.UserId, participation.EventId);
        return NoContent();
    }

    [HttpPost("cancel")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelParticipation(CreateEventParticipationRequest request)
    {
        var participation = _mapper.Map<EventParticipation>(request);
        await _service.CancelParticipationAsync(participation.UserId, participation.EventId);
        return NoContent();
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(List<EventParticipationResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetParticipations(Guid userId)
    {
        var participations = await _service.GetParticipationsByUserAsync(userId);
        var response = participations.Select(p => p.FromEntity()).ToList();
        return Ok(response);
    }
}
