using CommunityHub.API.DTOs.TagDTOs;
using CommunityHub.API.Extensions.Mappings;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : ControllerBase 
{
    private readonly ITagRepository _repository;

    public TagController(ITagRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<TagResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var tags = await _repository.GetAllAsync();
        var response = tags.Select(t => t.FromEntity()).ToList();
        return Ok(response);
    }
}
