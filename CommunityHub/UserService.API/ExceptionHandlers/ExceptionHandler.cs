using UserService.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace UserService.API.ExceptionHandlers;

public class ExceptionHandler : IExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        int statusCode;
        string title;

        switch (exception)
        {
            case NotFoundException notFoundException:
                statusCode = StatusCodes.Status404NotFound;
                title = notFoundException.Error;
                break;

            case BadRequestException notFoundException:
                statusCode = StatusCodes.Status400BadRequest;
                title = notFoundException.Error;
                break;

            case ConflictException conflictException:
                statusCode = StatusCodes.Status409Conflict;
                title = conflictException.Error;
                break;

            case UnauthorizedException unauthorizedException:
                statusCode = StatusCodes.Status401Unauthorized;
                title = unauthorizedException.Error;
                break;

            default:
                statusCode = StatusCodes.Status500InternalServerError;
                title = "An unexpected error occurred";
                break;
        }
        
        var problemDetails = new ProblemDetails
        {
            Title = title,
            Detail = exception.Message,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };

        _logger.LogError(exception,
            "Unhandled exception for {Method} {Path} with status = {StatusCode}",
            httpContext.Request.Method,
            httpContext.Request.Path,
            statusCode
            );

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}

