using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using CommunityHub.API.Models;

namespace CommunityHub.API.Data.Configurations;

public class TagConfiguration : IEntityTypeConfiguration<EventTag>
{
    public void Configure(EntityTypeBuilder<EventTag> builder)
    {
        builder.ToTable("Tag");
        builder.HasKey(t => t.Id);
        builder.Property(t => t.Name).IsRequired().HasMaxLength(100);
        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.UpdatedAt).IsRequired();
        builder.HasMany(t => t.Events)
               .WithMany(e => e.Tags)
               .UsingEntity<Dictionary<string, object>>(
                   "EventTag",
                   j => j.HasOne<Event>().WithMany().HasForeignKey("EventId"),
                   j => j.HasOne<EventTag>().WithMany().HasForeignKey("TagId"),
                   j =>
                   {
                       j.HasKey("EventId", "TagId");
                       j.ToTable("EventTags");
                   });
    }
}

