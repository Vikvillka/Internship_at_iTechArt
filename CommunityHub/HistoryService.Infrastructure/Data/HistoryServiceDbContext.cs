using HistoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

using HistoryService.Infrastructure.Data.Configurations;

namespace HistoryService.Infrastructure.Data;

public class HistoryServiceDbContext : DbContext
{
    public HistoryServiceDbContext(DbContextOptions<HistoryServiceDbContext> options)
        : base(options) { }

    public DbSet<HistoryRecord> HistoryRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new HistoryRecordConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}
