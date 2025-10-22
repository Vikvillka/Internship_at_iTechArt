using Gateway.API.Clients;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/community")]
public class CommunityGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;

    public CommunityGatewayController(IBestApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<CommunityResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllCommunitiesAsync();
        return Ok(result);
    }

    [HttpGet("get/{id:guid}")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _apiClient.GetCommunityByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCommunityRequest request)
    {
        var result = await _apiClient.CreateCommunityAsync(request);
        return Ok(result);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCommunityRequest request)
    {
        var result = await _apiClient.UpdateCommunityAsync(request);
        return Ok(result);
    }

    [HttpDelete("delete/{id:guid}")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _apiClient.DeleteCommunityByIdAsync(id);
        return Ok(result);
    }
}
