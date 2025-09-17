using CommunityHub.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommunityHub.API.Data.Configurations;

public class EventConfiguration
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Title).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Description).HasMaxLength(2000);
        builder.Property(e => e.EventDate).IsRequired();
        builder.Property(e => e.MaxParticipants).IsRequired();
        builder.Property(e => e.Venue).IsRequired().HasMaxLength(200);
        builder.Property(e => e.Address).IsRequired().HasMaxLength(200);
        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired();

        builder.HasQueryFilter(e => !e.Community.IsDeleted);
    }
}
