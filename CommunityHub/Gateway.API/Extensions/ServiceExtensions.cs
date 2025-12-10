using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

using Gateway.API.Clients;
using Gateway.API.Configurations;
using Gateway.API.Handlers;
using Gateway.API.Services.Сache;
using Gateway.API.Interfaces.Cache;

namespace Gateway.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddTransient<BasicAuthMessageHandler>();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();

        services.AddExceptionHandler<GatewayExceptionHandler>();
        services.AddProblemDetails();
        
        services.AddMemoryCache();
        services.AddScoped<IMemoryCacheApiService, MemoryCacheApiService>();
        
        services.AddRefitWithBasicAuth<IBestApiClient>(config);

        services.AddGrpcClients(config);

        services.AddAuthenticationSchemes(config);
        services.AddAuthorization();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
