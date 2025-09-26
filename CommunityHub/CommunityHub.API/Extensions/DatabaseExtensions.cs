using Microsoft.EntityFrameworkCore;

using CommunityHub.API.Data;
using CommunityHub.API.Initialization;
using CommunityHub.API.Repositories.Interfaces;
using CommunityHub.API.Models;

namespace CommunityHub.API.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommunityHubDbContext>();
        await db.Database.MigrateAsync();

        var communityRepo = scope.ServiceProvider.GetRequiredService<ICommunityRepository>();
        var tagRepo = scope.ServiceProvider.GetRequiredService<ITagRepository>();
        await DataInitializer.SeedAsync(communityRepo, tagRepo);
    }
}

