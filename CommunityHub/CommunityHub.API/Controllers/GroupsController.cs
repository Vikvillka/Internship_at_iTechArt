using CommunityHub.API.Initialization;
using CommunityHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        [HttpGet("getAll")]
        [ProducesResponseType(typeof(List<Group>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(DataStore.Groups);
        }

        [HttpGet("getAll/{id}")]
        [ProducesResponseType(typeof(List<Group>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(Guid id)
        {
            var group = DataStore.Groups.FirstOrDefault(g => g.Id == id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(List<Group>), StatusCodes.Status201Created)]
        public IActionResult Create([FromBody] Group group)
        {
            group.Id = Guid.NewGuid();
            DataStore.Groups.Add(group);
            return Ok(group);
        }

        [HttpPut("update")]
        [ProducesResponseType(typeof(List<Group>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Update([FromBody] Group updateGroup)
        {
            var group = DataStore.Groups.FirstOrDefault(g => g.Id == updateGroup.Id);
            if (group == null) return NotFound();

            group.Name = updateGroup.Name;
            group.Description = updateGroup.Description;
            group.Category = updateGroup.Category;
            group.City = updateGroup.City;
            group.Country = updateGroup.Country;
            group.Events = updateGroup.Events;

            return Ok(group);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(typeof(List<Group>), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteById(Guid id) 
        {
            var group = DataStore.Groups.FirstOrDefault(g => g.Id == id);
            if (group == null) return NotFound();
            DataStore.Groups.Remove(group);
            return Ok(group);
        }
    }
}
