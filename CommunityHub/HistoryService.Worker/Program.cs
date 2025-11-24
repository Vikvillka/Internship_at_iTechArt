using HistoryService.Worker.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration);
builder.AddServiceDefaults();

var host = builder.Build();
await host.MigrateDatabaseAsync();
host.Run();
