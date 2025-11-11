using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

using UserService.Infrastructure.Data;
using UserService.API.ExceptionHandlers;

namespace UserService.API.Extensions;

public static class ServiceExtensions
{
    public static void AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<UserServiceDbContext>(options =>
            options.UseNpgsql(config.GetConnectionString("DefaultConnection")));

        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddFluentValidationAutoValidation();

        services.AddAuthenticationSchemes(config);

        services.AddExceptionHandler<ExceptionHandler>();
        services.AddProblemDetails();
    }
}

