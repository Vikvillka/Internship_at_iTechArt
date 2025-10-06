using System.Diagnostics;

namespace CommunityHub.API.Middlewares;

public class TimerMiddleware
{
    private readonly RequestDelegate _next;

    public TimerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var watch = Stopwatch.StartNew();

        context.Response.OnStarting(() =>
        {
            watch.Stop();
            context.Response.Headers["X-Request-Duration"] = $"{watch.ElapsedMilliseconds}ms";
            return Task.CompletedTask;
        });

        await _next(context);
    }
}

