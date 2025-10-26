using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

using Gateway.API.Clients;
using Gateway.API.Configurations;
using Gateway.API.Extensions;
using Gateway.API.Filters;
using Gateway.API.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ExceptionFilter>();
});

builder.Services.AddTransient<BasicAuthMessageHandler>();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfiguration>();

builder.Services.AddRefitWithBasicAuth<IBestApiClient>(builder.Configuration, builder.Configuration["ApiBaseUrl"]!);
builder.Services.AddRefitWithBasicAuth<IUserApiClient>(builder.Configuration, builder.Configuration["ApiBaseUrl"]!);

builder.Services.AddAuthenticationSchemes(builder.Configuration);
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
