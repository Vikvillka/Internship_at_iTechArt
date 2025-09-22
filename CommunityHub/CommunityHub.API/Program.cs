using CommunityHub.API.Data;
using CommunityHub.API.Extensions;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommunityHubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<Community>, EfRepository<Community>>();
builder.Services.AddScoped<ICommunityRepository<Community>, CommunityRepository<Community>>();

// There is a small problem.
// I tried to fix the retrieval of Communities with Events, but I ended up with a cyclic entity dependency.
// I am ignoring the cycles, but I suggest switching to DTOs or are there other methods?
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;
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
