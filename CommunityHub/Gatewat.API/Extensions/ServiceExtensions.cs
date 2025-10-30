using Gateway.API.Clients;
using Gateway.API.Configurations;
using Gateway.API.Handlers;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Gateway.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();

        services.AddTransient<BasicAuthMessageHandler>();
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();

        services.AddExceptionHandler<GatewayExceptionHandler>();
        services.AddProblemDetails();

        services.AddRefitWithBasicAuth<IBestApiClient>(config, config["ApiBaseUrl"]!);
        services.AddRefitWithBasicAuth<IUserApiClient>(config, config["ApiBaseUrl"]!);

        services.AddAuthenticationSchemes(config);
        services.AddAuthorization();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }
}
