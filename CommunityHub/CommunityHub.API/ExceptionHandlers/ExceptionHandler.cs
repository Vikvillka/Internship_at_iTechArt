using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Net;

namespace CommunityHub.API.ExceptionHandlers;

public class ExceptionHandler : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var statusCode = (int)HttpStatusCode.InternalServerError;
        
        var problemDetails = new ProblemDetails
        {
            Title = "An unexpected error occurred",
            Detail = exception.Message,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };

        Log.Error(exception,
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

