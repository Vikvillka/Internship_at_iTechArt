using Microsoft.EntityFrameworkCore;

using HistoryService.Infrastructure.Data;

namespace HistoryService.Worker.Extensions;

public static class DatabaseExtensions
{
    public static async Task MigrateDatabaseAsync(this IHost host) 
    {
        using var scope = host.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<HistoryServiceDbContext>();
        await db.Database.MigrateAsync();
    }
}
