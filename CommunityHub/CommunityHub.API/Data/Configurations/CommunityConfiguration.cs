using CommunityHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityHub.API.Data.Configurations;

public class CommunityConfiguration : IEntityTypeConfiguration<Community>
{
    public void Configure(EntityTypeBuilder<Community> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
        builder.Property(c => c.Description).HasMaxLength(1500);
        builder.Property(c => c.Category).IsRequired().HasMaxLength(50);
        builder.Property(c => c.City).IsRequired().HasMaxLength(50);
        builder.Property(c => c.Country).IsRequired().HasMaxLength(50);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.UpdatedAt).IsRequired();
        builder.Property(c => c.IsDeleted).IsRequired().HasDefaultValue(false);

        builder.HasMany(c => c.Events)
               .WithOne()
               .HasForeignKey(e => e.CommunityId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasQueryFilter(c => !c.IsDeleted);

        builder.HasIndex(c => c.IsDeleted).HasFilter("\"IsDeleted\" = false");
    }
}

