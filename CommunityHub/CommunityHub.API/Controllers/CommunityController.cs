using CommunityHub.API.DataSources;
using CommunityHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
    private readonly IDataSource<Community> _dataSource;

    public CommunityController(IDataSource<Community> dataSource)
    {
        _dataSource = dataSource;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<Community>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var communities = await _dataSource.GetAllAsync();
        return Ok(communities.Where(c => !c.IsDeleted));
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var community = await _dataSource.GetByIdAsync(id);
        if (community == null || community.IsDeleted) return NotFound();
        return Ok(community);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] Community community)
    {
        var created = await _dataSource.CreateAsync(community);
        return Ok(created);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] Community update—ommunity)
    {
        var success = await _dataSource.UpdateAsync(update—ommunity);
        if (!success) return BadRequest();
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var success = await _dataSource.DeleteAsync(id);
        if (!success) return BadRequest();
        return NoContent();
    }
}

