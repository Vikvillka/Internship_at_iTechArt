using Microsoft.AspNetCore.Http;

using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Contracts.DTOs.ImageDTOs;

namespace CommunityHub.Infrastructure.Services;

public class ImageService : IImageService
{
    public async Task<ImageUploadResponse> UploadAsync(IFormFile file, string storagePath)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File not found", nameof(file));

        Directory.CreateDirectory(storagePath);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var filePath = Path.Combine(storagePath, fileName);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream);

        var imageUrl = $"{fileName}";
        return new ImageUploadResponse { ImageUrl = imageUrl };
    }

    public async Task<bool> DeleteAsync(string fileName, string storagePath)
    {
        if(string.IsNullOrEmpty(fileName))
            throw new ArgumentException("Invalid file name", nameof(fileName));
        
        var filePath = Path.Combine(storagePath, fileName);

        if (!File.Exists(filePath))
            return await Task.FromResult(false);

        File.Delete(filePath);
        return await Task.FromResult(true);
    }
}
