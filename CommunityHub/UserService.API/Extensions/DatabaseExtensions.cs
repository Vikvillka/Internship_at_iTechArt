using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Data;

namespace UserService.API.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<UserServiceDbContext>();
        await db.Database.MigrateAsync();
    }
}
