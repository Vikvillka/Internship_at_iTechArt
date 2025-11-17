using AutoMapper;
using Gateway.API.Clients;
using Gateway.API.DTOs.ParticipantionDTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.GRpc;

namespace Gateway.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ParticipationController : ControllerBase
{
    private readonly ParticipationService.ParticipationServiceClient _apiClient;
    private readonly IBestApiClient _eventClient;
    private readonly IMapper _mapper;

    public ParticipationController(
        ParticipationService.ParticipationServiceClient apiClient,
        IBestApiClient eventClient,
        IMapper mapper)
    {
        _apiClient = apiClient;
        _eventClient = eventClient;
        _mapper = mapper;
    }

    [HttpPost("participate")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(GatewayParticipationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Participate(GatewayCreateParticipationRequest request)
    {
        var eventItem = await _eventClient.GetEventByIdAsync(request.EventId);
        if (eventItem == null)
            return NotFound($"Event with id {request.EventId} not found");

        var grpcRequest = _mapper.Map<CreateParticipationRequest>(request);
        var grpcReply = await _apiClient.ParticipateAsync(grpcRequest);
        var response = _mapper.Map<GatewayParticipationResponse>(grpcReply);

        response.EventName = eventItem.Title;

        return Ok(response);
    }

    [HttpPost("cancel")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> CancelParticipation(GatewayCreateParticipationRequest request)
    {
        var eventItem = await _eventClient.GetEventByIdAsync(request.EventId);
        if (eventItem == null)
            return NotFound($"Event with id {request.EventId} not found");

        var grpcRequest = _mapper.Map<CreateParticipationRequest>(request);
        bool success = _mapper.Map<bool>(await _apiClient.CancelParticipationAsync(grpcRequest));

        return success ? NoContent() : NotFound();
    }

    [HttpGet("user/{userId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(List<GatewayParticipationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserParticipations(Guid userId)
    {
        var grpcReply = await _apiClient.GetParticipationsByUserAsync(
            new GetUserParticipationsRequest { UserId = userId.ToString()});

        var gatewayList = _mapper.Map<List<GatewayParticipationResponse>>(grpcReply);

        foreach (var part in gatewayList)
        {
            var eventItem = await _eventClient.GetEventByIdAsync(part.EventId);
            part.EventName = eventItem.Title;
        }

        return Ok(gatewayList);
    }
}