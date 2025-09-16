using CommunityHub.API.Initialization;
using CommunityHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers
{
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
        [ProducesResponseType(typeof(Group), StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] Group group)
        {
            DataStorage.AddGroup(group);
            return CreatedAtAction(nameof(GetById), new { id = group.Id }, group);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(Group), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] Group updateGroup)
        {
            var success = DataStorage.UpdateGroup(updateGroup);
            if (!success) return NotFound();

            return Ok(updateGroup);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(Group), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteById(Guid id) 
        {
            var success = DataStorage.DeleteGroup(id);
            if (!success) return NotFound();
            return NoContent();
        }
    }
}
