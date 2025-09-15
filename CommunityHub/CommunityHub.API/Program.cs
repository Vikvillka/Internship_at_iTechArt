using CommunityHub.API;
using CommunityHub.API.Controllers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

DataInitializer.Seed(GroupsController.Groups);

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
