using CommunityHub.Contracts.DTOs.TagDTOs;
using Gateway.API.Clients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [AllowAnonymous]
    [ProducesResponseType(typeof(List<TagResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var result = await _apiClient.GetAllTagsAsync();
        return Ok(result);
    }
}
