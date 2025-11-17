using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.Contracts.DTOs.UserDTOs;
using CommunityHub.API.Extensions.Mappings;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Basic")]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService service, IMapper mapper, ILogger<UserController> logger)
    {
        _service = service;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] CreateUserRequest request)
    {
        _logger.LogInformation("Try to register a user");
        var user = _mapper.Map<User>(request);
        var createdUser = await _service.RegisterAsync(user, request.Password);
        var response = createdUser.FromEntity();
        return Ok(response);
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
