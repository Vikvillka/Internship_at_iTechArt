using CommunityHub.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers
{
    [ApiController]
    [Route("groups")]
    public class GroupsController : ControllerBase
    {
        public static List<Group> Groups = new();

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            return Ok(Groups);
        }

        [HttpGet("getAll/{id}")]
        public IActionResult GetById(Guid id)
        {
            var group = Groups.FirstOrDefault(g => g.Id == id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] Group group)
        {
            group.Id = Guid.NewGuid();
            Groups.Add(group);
            return Ok(group);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] Group updateGroup)
        {
            var group = Groups.FirstOrDefault(g => g.Id == updateGroup.Id);
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
        public IActionResult DeleteById(Guid id) 
        {
            var group = Groups.FirstOrDefault(g => g.Id == id);
            if (group == null) return NotFound();
            Groups.Remove(group);
            return Ok(group);
        }
    }
}
