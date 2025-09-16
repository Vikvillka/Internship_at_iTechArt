using CommunityHub.API.Initialization;
using CommunityHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<Community>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(DataStorage.GetAllCommunities());
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var group = DataStorage.GetCommunityById(id);
        if (group == null) return NotFound();
        return Ok(group);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] Community group)
    {
        DataStorage.AddCommunity(group);
        return Ok(group);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(Community), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] Community updateGroup)
    {
        var success = DataStorage.UpdateCommunity(updateGroup);
        if (!success) return BadRequest();

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteById(Guid id)
    {
        var success = DataStorage.DeleteCommunity(id);
        if (!success) return BadRequest();
        return NoContent();
    }
}

