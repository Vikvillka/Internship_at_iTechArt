using Microsoft.AspNetCore.Diagnostics;
using Refit;
using System.Text.Json;

namespace Gateway.API.Handlers;

public class GatewayExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GatewayExceptionHandler> _logger;

    public GatewayExceptionHandler(ILogger<GatewayExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        Microsoft.AspNetCore.Mvc.ProblemDetails problemDetails;
        int statusCode = StatusCodes.Status500InternalServerError;

        if (exception is ApiException apiException)
        {
            statusCode = (int)apiException.StatusCode;
            object? contentObj = null;
            if (!string.IsNullOrEmpty(apiException.Content))
                contentObj = JsonSerializer.Deserialize<object>(apiException.Content);

            problemDetails = new()
            {
                Title = "CommunityHub.API Error",
                Detail = contentObj?.ToString() ?? apiException.Message,
                Status = statusCode,
                Instance = httpContext.Request.Path
            };

            _logger.LogWarning(apiException,
                "CommunityHub.API exception for {Method} {Path} with status = {Status}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                statusCode);
        }
        else
        {
            problemDetails = new()
            {
                Title = "Internal Server Error",
                Detail = exception.Message,
                Status = StatusCodes.Status500InternalServerError,
                Instance = httpContext.Request.Path
            };

            _logger.LogError(exception,
                "Unhandled exception for {Method} {Path} with status = {StatusCode}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                statusCode
                );
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;
        
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
