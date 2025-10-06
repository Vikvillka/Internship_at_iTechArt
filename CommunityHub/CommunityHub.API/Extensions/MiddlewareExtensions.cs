using CommunityHub.API.Middlewares;

namespace CommunityHub.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseResponseTimer(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimerMiddleware>();
    }
}

