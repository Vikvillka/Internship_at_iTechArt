using UserService.GRpc.Extensions;
using UserService.GRpc.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);
builder.AddServiceDefaults();

if (builder.Configuration.GetValue<bool>("IsRunOnAspire"))
    builder.AddRedisDistributedCache("redis");
else
    builder.Services.AddStackExchangeRedisCache(o =>
        o.Configuration = builder.Configuration.GetConnectionString("redis"));

var app = builder.Build();

await app.MigrateDatabaseAsync();

app.MapGrpcService<UserServiceImpl>();
app.MapGrpcService<AuthServiceImpl>();
app.MapGrpcService<ParticipationServiceImpl>();
app.MapGrpcService<SubscriptionServiceImpl>();

app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
