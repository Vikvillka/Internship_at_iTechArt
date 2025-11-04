using Microsoft.AspNetCore.Http;

using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Contracts.DTOs.ImageDTOs;

namespace CommunityHub.Infrastructure.Services;

public class ImageService : IImageService
{
    public async Task<ImageUploadResponse> UploadAsync(IFormFile file, string storagePath)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Файл не найден", nameof(file));

        Directory.CreateDirectory(storagePath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(storagePath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var imageUrl = $"/images/{fileName}";
        return new ImageUploadResponse { ImageUrl = imageUrl };
    }
}
