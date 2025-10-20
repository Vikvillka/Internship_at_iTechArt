using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Refit;
using System.Text.Json;

namespace Gateway.API.Filters;

public class ExceptionFilter : IAsyncExceptionFilter
{
    public Task OnExceptionAsync(ExceptionContext context)
    {
        object? contentObj = null;
        
        if (context.Exception is ApiException ex)
        {
            var statusCode = (int)ex.StatusCode;
            if (!string.IsNullOrEmpty(ex.Content))
                contentObj = JsonSerializer.Deserialize<object>(ex.Content);

            context.Result = new JsonResult(contentObj)
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
        else
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            contentObj = new
            {
                title = "InternalServerError",
                status = statusCode,
                detail = context.Exception.Message,
                instance = context.HttpContext.Request.Path
            };

            context.Result = new JsonResult(contentObj)
            {
                StatusCode = statusCode
            };
            context.ExceptionHandled = true;
        }
        return Task.CompletedTask;
    }
}
