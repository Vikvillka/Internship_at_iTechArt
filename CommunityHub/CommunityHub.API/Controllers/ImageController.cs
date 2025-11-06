using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Contracts.DTOs.ImageDTOs;

namespace CommunityHub.API.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Basic")]
public class ImageController : ControllerBase
{
    private readonly IImageService _imageService;
    private readonly string _imageStoragePath;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
        _imageStoragePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
    }

    [HttpGet("{fileName}")]
    [Produces("image/jpeg", "image/png", "image/gif")]
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
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] ImageUploadRequest request)
    {
        var result = await _imageService.UploadAsync(request.File, _imageStoragePath);
        return Ok(result);
    }

    [HttpDelete("delete/{fileName}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string fileName)
    {
        var isDeleted = await _imageService.DeleteAsync(fileName, _imageStoragePath);
        if (!isDeleted)
            return NotFound();

        return NoContent();
    }
}
