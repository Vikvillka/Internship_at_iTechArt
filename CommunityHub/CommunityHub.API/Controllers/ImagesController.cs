using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Contracts.DTOs.ImageDTOs;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ImagesController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly string _imageStoragePath;

    public ImagesController(IImageService imageService)
    {
        _imageService = imageService;
        _imageStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
    }

    [HttpGet("{fileName:string}")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetImage(string fileName)
    {
        var filePath = Path.Combine(_imageStoragePath, fileName);
        if (!System.IO.File.Exists(filePath))
            return NotFound();
        var contentType = "image/" + Path.GetExtension(fileName).Trim('.');
        var stream = System.IO.File.OpenRead(filePath);
        return File(stream, contentType);
    }

    [HttpPost("upload")]
    [ProducesResponseType(typeof(ImageUploadResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        var result = await _imageService.UploadAsync(file, _imageStoragePath);
        return Ok(result);
    }
}
