using HistoryService.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.AddServiceDefaults();
//builder.Services.AddHostedService<Worker>();

var host = builder.Build();
await host.MigrateDatabaseAsync();
host.Run();
