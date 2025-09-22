using AutoMapper;
using CommunityHub.API.DTOs;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommunityController : ControllerBase
{
    private readonly ICommunityRepository<Community> _repository;
    private readonly IMapper _mapper;

    public CommunityController(ICommunityRepository<Community> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet("getAll")]
    [ProducesResponseType(typeof(List<CommunityResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        var communities = await _repository.GetAllAsync();
        var response = communities.Select(CommunityResponse.FromModel).ToList();
        return Ok(response);
    }

    [HttpGet("get/{id}")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var community = await _repository.GetByIdAsync(id);
        if (community == null) return NotFound();
        
        var response = CommunityResponse.FromModel(community);
        return Ok(response);
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Create([FromBody] CreateCommunityRequest request)
    {
        var communityEntity = _mapper.Map<Community>(request);
        var created = await _repository.CreateAsync(communityEntity);
        var response = CommunityResponse.FromModel(created);
        return Ok(response);
    }

    [HttpPut("update")]
    [ProducesResponseType(typeof(CommunityResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] CommunityResponse request)
    {
        var entityUpdate = _mapper.Map<Community>(request);
        var success = await _repository.UpdateAsync(entityUpdate);
        
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