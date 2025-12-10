using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CommunityHub.Contracts.DTOs.ImageDTOs;

public class ImageUploadRequest
{
    [FromForm(Name = "file")]
    public IFormFile File { get; set; } = default!;
}
