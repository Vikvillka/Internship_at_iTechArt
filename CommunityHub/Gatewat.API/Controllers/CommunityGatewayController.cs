using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Gateway.API.Clients;
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
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<CommunityResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllCommunitiesAsync();
        return Ok(result);
    }

    [HttpGet("get/{id:guid}")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var result = await _apiClient.GetCommunityByIdAsync(id);
        return Ok(result);
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create(CreateCommunityRequest request)
    {
        var result = await _apiClient.CreateCommunityAsync(request);
        return Ok(result);
    }

    [HttpPut("update")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update(UpdateCommunityRequest request)
    {
        var result = await _apiClient.UpdateCommunityAsync(request);
        return Ok(result);
    }

    [HttpDelete("delete/{id:guid}")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _apiClient.DeleteCommunityByIdAsync(id);
        return Ok(result);
    }
}
