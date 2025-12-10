using Gateway.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

if (builder.Configuration.GetValue<bool>("IsRunOnAspire"))
    builder.AddRedisDistributedCache("redis");
else
    builder.Services.AddStackExchangeRedisCache(o =>
        o.Configuration = builder.Configuration.GetConnectionString("redis"));

builder.AddServiceDefaults();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
