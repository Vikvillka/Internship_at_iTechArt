using CommunityHub.API.Middlewares;

namespace CommunityHub.API.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TimerMiddleware>();
    }
}

