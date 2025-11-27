using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data.Configurations;

public class EventParticipationConfiguration : IEntityTypeConfiguration<EventParticipation>
{
    public void Configure(EntityTypeBuilder<EventParticipation> builder)
    {
        builder.Property(p => p.UserId).IsRequired();
        builder.Property(p => p.EventId).IsRequired();

        builder.HasIndex(p => new { p.UserId, p.EventId }).IsUnique();
    }
}
