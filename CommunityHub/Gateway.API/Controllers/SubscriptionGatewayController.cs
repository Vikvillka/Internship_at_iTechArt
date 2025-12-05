using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using UserService.GRpc;
using Gateway.API.Clients;
using Gateway.API.DTOs.SubscriptionDTOs;
using Gateway.API.Interfaces;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/subscription")]
public class SubscriptionGatewayController : ControllerBase
{
    private readonly ISubscriptionGrpcClient _apiClient;
    private readonly IBestApiClient _communityClient;
    private readonly IMapper _mapper;

    public SubscriptionGatewayController(
        ISubscriptionGrpcClient grpcClient,
        IBestApiClient communityClient,
        IMapper mapper)
    {
        _apiClient = grpcClient;
        _communityClient = communityClient;
        _mapper = mapper;
    }

    [HttpPost("subscribe")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(GatewaySubscriptionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Subscribe(GatewayCreateSubscriptionRequest request)
    {
        var community = await _communityClient.GetCommunityByIdAsync(request.CommunityId);
        if (community == null)
            return NotFound($"Community with id {request.CommunityId} not found");

        var grpcRequest = _mapper.Map<CreateSubscriptionRequest>(request);
        var grpcReply = await _apiClient.SubscribeAsync(grpcRequest);
        var gatewayResponse = _mapper.Map<GatewaySubscriptionResponse>(grpcReply);

        gatewayResponse.CommunityName = community.Name;

        return Ok(gatewayResponse);
    }

    [HttpPatch("unsubscribe")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Unsubscribe(GatewayCreateSubscriptionRequest request)
    {
        var community = await _communityClient.GetCommunityByIdAsync(request.CommunityId);
        if (community == null)
            return NotFound($"Community with id {request.CommunityId} not found");

        var grpcRequest = _mapper.Map<CreateSubscriptionRequest>(request);
        var grpcReply = await _apiClient.UnsubscribeAsync(grpcRequest);
        var success = _mapper.Map<bool>(grpcReply);

        return success ? NoContent() : NotFound();
    }

    [HttpGet("user/{userId:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(List<GatewaySubscriptionResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserSubscriptions(Guid userId)
    {
        var grpcReply = await _apiClient.GetSubscriptionsByUserAsync(
            new GetUserSubscriptionsRequest { UserId = userId.ToString()});

        var gatewayList = _mapper.Map<List<GatewaySubscriptionResponse>>(grpcReply);

        foreach (var sub in gatewayList)
        {
            var community = await _communityClient.GetCommunityByIdAsync(sub.CommunityId);
            sub.CommunityName = community.Name;
        }

        return Ok(gatewayList);
    }

    [HttpGet("community/{communityId:guid}/count")]
    [ProducesResponseType(typeof(GatewayCommunitySubscriptionsCountResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetEventParticipantsCount(Guid communityId)
    {
        var grpcReply = await _apiClient.GetCommunitySubscriptionsCountAsync(
            new GetCommunitySubscriptionsCountRequest
            {
                CommunityId = communityId.ToString()
            });

        var response = _mapper.Map<GatewayCommunitySubscriptionsCountResponse>(grpcReply);
        response.CommunityId = communityId;

        return Ok(response);
    }
}
