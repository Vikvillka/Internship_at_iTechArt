using Gateway.API.Clients;
using Microsoft.AspNetCore.Mvc;

namespace Gateway.API.Controllers;

[ApiController]
[Route("gateway/image")]
public class ImageGatewayController : ControllerBase
{
    private readonly IBestApiClient _apiClient;

    public ImageGatewayController(IBestApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    [Produces("image/jpeg", "image/png", "image/gif")]
    [ProducesResponseType(typeof(FileStreamResult), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetImage(string fileName)
    {
        var response = await _apiClient.GetImageAsync(fileName);

        if (!response.IsSuccessStatusCode || response.Content == null)
            return NotFound();

        var stream = await response.Content.ReadAsStreamAsync();
        var contentType = response.Content.Headers.ContentType?.MediaType;

        return File(stream, contentType);
    }
}
