using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UserService.API.Extensions.Mappings;
using UserService.Application.Intarfaces.Services;
using UserService.Contracts.DTOs.SubscriptionDTOs;
using UserService.Domain.Entities;

namespace UserService.API.Controllers;

[ApiController]
[Route("subscription")]
[Authorize(AuthenticationSchemes = "Basic")]
public class CommunitySubscriptionController : ControllerBase
{
    private readonly ICommunitySubscriptionService _service;
    private readonly IMapper _mapper;

    public CommunitySubscriptionController(ICommunitySubscriptionService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpPost("subscribe")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Subscribe(CreateCommunitySubscriptionRequest request)
    {
        var sub = _mapper.Map<CommunitySubscription>(request);
        await _service.SubscribeAsync(sub.UserId, sub.CommunityId);
        return NoContent();
    }

    [HttpPost("unsubscribe")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Unsubscribe([FromBody] CreateCommunitySubscriptionRequest request)
    {
        var unsub = _mapper.Map<CommunitySubscription>(request);
        await _service.UnsubscribeAsync(unsub.UserId, unsub.CommunityId);
        return NoContent();
    }

    [HttpGet("user/{userId:guid}")]
    [ProducesResponseType(typeof(List<CommunitySubscriptionResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSubscriptions(Guid userId)
    {
        var subs = await _service.GetSubscriptionsByUserAsync(userId);
        var response = subs.Select(s => s.FromEntity()).ToList();
        return Ok(response);
    }
}
