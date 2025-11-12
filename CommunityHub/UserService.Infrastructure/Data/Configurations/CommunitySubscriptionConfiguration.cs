using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data.Configurations;

public class CommunitySubscriptionConfiguration : IEntityTypeConfiguration<CommunitySubscription>
{
    public void Configure(EntityTypeBuilder<CommunitySubscription> builder)
    {
        builder.Property(s => s.UserId).IsRequired();
        builder.Property(s => s.CommunityId).IsRequired();
        
        builder.HasIndex(s => new { s.UserId, s.CommunityId }).IsUnique();
    }
}
