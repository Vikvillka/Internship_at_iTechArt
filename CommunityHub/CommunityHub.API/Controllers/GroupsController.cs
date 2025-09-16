using CommunityHub.API.Initialization;
using CommunityHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroupsController : ControllerBase
{
    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<Group>), StatusCodes.Status200OK)]
    public IActionResult GetAll()
    {
        return Ok(DataStorage.GetAllGroups());
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetById(Guid id)
    {
        var group = DataStorage.GetGroupById(id);
        if (group == null) return NotFound();
        return Ok(group);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
    public IActionResult Create([FromBody] Group group)
    {
        DataStorage.AddGroup(group);
        return Ok(group);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult Update([FromBody] Group updateGroup)
    {
        var success = DataStorage.UpdateGroup(updateGroup);
        if (!success) return BadRequest();

        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult DeleteById(Guid id)
    {
        var success = DataStorage.DeleteGroup(id);
        if (!success) return BadRequest();
        return NoContent();
    }
}

