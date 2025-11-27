using Gateway.API.Extensions.Mappings.Convertors;
using Grpc.Core;
using Microsoft.AspNetCore.Diagnostics;
using Refit;
using System.Text.Json.Nodes;

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

        switch (exception)
        {
            case ValidationApiException validationException:
                {
                    var problem = validationException.Content;

                    problemDetails = new()
                    {
                        Title = problem?.Title ?? "Validation error",
                        Status = problem?.Status ?? StatusCodes.Status400BadRequest,
                        Instance = httpContext.Request.Path
                    };

                    if (problem?.Errors is not null)
                        problemDetails.Extensions["errors"] = problem.Errors;

                    LogWarning(problemDetails.Title, problemDetails.Status ?? statusCode, httpContext);
                    break;
                }
            case ApiException apiException:
                {
                    statusCode = (int)apiException.StatusCode;
                    JsonNode? contentObj = null;
                    if (!string.IsNullOrEmpty(apiException.Content))
                        contentObj = JsonNode.Parse(apiException.Content);

                    problemDetails = new()
                    {
                        Title = contentObj?["title"]?.ToString() ?? "CommunityHub.API error",
                        Detail = contentObj?["detail"]?.ToString() ?? apiException.Message,
                        Status = statusCode,
                        Instance = httpContext.Request.Path
                    };

                    LogWarning(problemDetails.Title, problemDetails.Status ?? statusCode, httpContext);
                    break;
                }
            case GrpcProblemDetailsException grpcException:
                {
                    problemDetails = new()
                    {
                        Title = grpcException.Title,
                        Detail = grpcException.Message,
                        Status = grpcException.Status,
                        Instance = grpcException.Instance ?? httpContext.Request.Path
                    };

                    statusCode = grpcException.Status;
                    LogWarning(problemDetails.Title, problemDetails.Status ?? statusCode, httpContext);
                    break;
                }
            default:
                {
                    problemDetails = new()
                    {
                        Title = "Internal Server Error",
                        Detail = exception.Message,
                        Status = statusCode,
                        Instance = httpContext.Request.Path
                    };

                    _logger.LogError(exception,
                        "Unhandled exception for {Method} {Path} with status = {StatusCode}",
                        httpContext.Request.Method,
                        httpContext.Request.Path,
                        problemDetails.Status
                        );
                    break;
                }
        }

        httpContext.Response.StatusCode = problemDetails.Status ?? statusCode;
        
        await httpContext.Response
            .WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }

    private void LogWarning(string title, int statusCode, HttpContext context)
    {
        _logger.LogWarning(
            "{Title} for {Method} {Path} with status = {statusCode}",
            title,
            context.Request.Method,
            context.Request.Path,
            statusCode
        );
    }
}
