using AutoMapper;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.API.DTOs.CommunitiesDTOs;
using CommunityHub.API.Extensions.Mappings;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
    private readonly ICommunityService _service;
    private readonly IMapper _mapper;

    public CommunityController(ICommunityService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<CommunityResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var communities = await _service.GetAllAsync();
        var response = communities.Select(c => c.FromEntity()).ToList();
        return Ok(response);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var community = await _service.GetByIdAsync(id);
        if (community == null) return NotFound();
        
        var response = community.FromEntity();
        return Ok(response);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCommunityRequest request)
    {
        var communityEntity = _mapper.Map<Community>(request);
        var created = await _service.CreateAsync(communityEntity);
        var response = created.FromEntity();
        return Ok(response);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] UpdateCommunityRequest request)
    {
        var entityUpdate = _mapper.Map<Community>(request);
        var success = await _service.UpdateAsync(entityUpdate);
        if (!success) return BadRequest();
        return Ok();
    }

    [HttpDelete("delete/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteById(Guid id)
    {
        var success = await _service.DeleteAsync(id);
        if (!success) return BadRequest();
        return NoContent();
    }
}