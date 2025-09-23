using Microsoft.EntityFrameworkCore;

using CommunityHub.API.Models;

namespace CommunityHub.API.Data;

public class CommunityHubDbContext : DbContext
{
    public CommunityHubDbContext(DbContextOptions<CommunityHubDbContext> options)
        : base(options) {}

    public DbSet<Community> Communities { get; set; }
    public DbSet<Event> Events { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    { 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CommunityHubDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyTimestampsAndSoftDelete();
        return await base.SaveChangesAsync(cancellationToken);
    }

    private void ApplyTimestampsAndSoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    break;
                case EntityState.Deleted:
                    if (entry.Entity is Community community)
                    {
                        entry.State = EntityState.Modified;
                        community.IsDeleted = true;
                        community.UpdatedAt = DateTime.UtcNow;
                    }
                    break;
            }
        }
    }
}

