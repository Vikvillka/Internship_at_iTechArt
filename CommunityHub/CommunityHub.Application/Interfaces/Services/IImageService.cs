using Microsoft.AspNetCore.Http;

using CommunityHub.Contracts.DTOs.ImageDTOs;

namespace CommunityHub.Application.Interfaces.Services;

public interface IImageService
{
    Task<ImageUploadResponse> UploadAsync(IFormFile file, string storagePath);
}
