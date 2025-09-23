using Microsoft.EntityFrameworkCore;

using CommunityHub.API.Data;
using CommunityHub.API.Initialization;
using CommunityHub.API.Models;
using CommunityHub.API.Repositories.Interfaces;

namespace CommunityHub.API.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CommunityHubDbContext>();
        await db.Database.MigrateAsync();

        var repository = scope.ServiceProvider.GetRequiredService<IRepository<Community>>();
        await DataInitializer.SeedAsync(repository);
    }
}

