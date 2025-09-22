using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
    private readonly ICommunityRepository<Community> _repository;

    public CommunityController(ICommunityRepository<Community> dataSource)
    {
        _repository = dataSource;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<Community>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var communities = await _repository.GetAllAsync();
        return Ok(communities);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var community = await _repository.GetByIdAsync(id);
        if (community == null) return NotFound();
        return Ok(community);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] Community community)
    {
        var created = await _repository.CreateAsync(community);
        return Ok(created);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] Community updateCommunity)
    {
        var success = await _repository.UpdateAsync(updateCommunity);
        if (!success) return BadRequest();
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var success = await _repository.DeleteAsync(id);
        if (!success) return BadRequest();
        return NoContent();
    }
}

