using CommunityHub.API.Data;
using CommunityHub.API.DataSources;
using CommunityHub.API.Initialization;
using CommunityHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommunityHubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDataSource<Community>, EfDataSource<Community>>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//DataInitializer.Seed();

using (var scope = app.Services.CreateScope())
{
    // I don't remember what was said about seed data with EF, so I will leave it like this for now
    var db = scope.ServiceProvider.GetRequiredService<CommunityHubDbContext>();
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
