using Refit;

using Gateway.API.Handlers;

namespace Gateway.API.Extensions;

public static class RefitExtensions 
{
    public static IHttpClientBuilder AddRefitWithBasicAuth<T>(this IServiceCollection services, IConfiguration config) where T : class
    {
        var baseUrl = config["CommunityServiceApi:ApiBaseUrl"];

        if (string.IsNullOrWhiteSpace(baseUrl))
            throw new InvalidOperationException("ApiBaseUrl is not configured");

        return services.AddRefitClient<T>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl))
            .AddHttpMessageHandler(() => new BasicAuthMessageHandler(config));
    }
}
