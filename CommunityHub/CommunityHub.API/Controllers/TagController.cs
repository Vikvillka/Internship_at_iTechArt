using Microsoft.AspNetCore.Mvc;

using CommunityHub.API.DTOs.TagDTOs;
using CommunityHub.API.Extensions.Mappings;
using CommunityHub.Application.Interfaces.Services;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase 
{
    private readonly ITagService _service;

    public TagController(ITagService service)
    {
        _service = service;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<TagResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _service.GetAllAsync();
        var response = tags.Select(t => t.FromEntity()).ToList();
        return Ok(response);
    }
}
