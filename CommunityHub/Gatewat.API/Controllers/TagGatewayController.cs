using Gateway.API.Clients;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.Contracts.DTOs.TagDTOs;
using Microsoft.AspNetCore.Authorization;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/tag")]
public class TagGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;

    public TagGatewayController(IBestApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<TagResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllTagsAsync();
        return Ok(result);
    }
}
