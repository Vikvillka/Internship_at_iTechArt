using AutoMapper;
using CommunityHub.API.Data;
using CommunityHub.API.Extensions;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories;
using CommunityHub.API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CommunityHubDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRepository<Community>, EfRepository<Community>>();
builder.Services.AddScoped<ICommunityRepository<Community>, CommunityRepository<Community>>();

var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddMaps(Assembly.GetExecutingAssembly());
});

IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
// продумать как добавлять евенты вместе с комьюнити 
await app.MigrateDatabaseAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
