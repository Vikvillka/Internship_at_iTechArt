using Refit;

using Gateway.API.Handlers;

namespace Gateway.API.Extensions;

public static class RefitExtensions 
{
    public static IHttpClientBuilder AddRefitWithBasicAuth<T>(this IServiceCollection services, IConfiguration config, string baseUrl) where T : class
    {
        return services.AddRefitClient<T>()
            .ConfigureHttpClient(c => c.BaseAddress = new Uri(baseUrl))
            .AddHttpMessageHandler(() => new BasicAuthMessageHandler(config));
    }
}
