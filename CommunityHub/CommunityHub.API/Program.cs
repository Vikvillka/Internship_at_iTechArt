using Microsoft.EntityFrameworkCore;
using System.Reflection;

using CommunityHub.API.Data;
using CommunityHub.API.Extensions;
using CommunityHub.API.Repositories;
using CommunityHub.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommunityHubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICommunityRepository, CommunityRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
 
await app.MigrateDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
