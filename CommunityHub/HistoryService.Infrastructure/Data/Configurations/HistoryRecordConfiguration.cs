using HistoryService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HistoryService.Infrastructure.Data.Configurations;

public class HistoryRecordConfiguration : IEntityTypeConfiguration<HistoryRecord>
{
    public void Configure(EntityTypeBuilder<HistoryRecord> builder)
    {
        builder.Property(h => h.HistoryEventType).IsRequired();
        builder.Property(h => h.Date).IsRequired();
        builder.Property(h => h.Payload).IsRequired();
        builder.Property(h => h.TriggeredBy).IsRequired();

        builder.HasIndex(h => h.Date);
    }
}
