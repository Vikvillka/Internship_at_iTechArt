using Microsoft.Extensions.Logging;
using System.Text.Json;

using HistoryService.Contracts.HistoryRecordDTOs;
using HistoryService.Domain.Entities;
using HistoryService.Infrastructure.Data;
using HistoryService.Application.Intefaces.RabbitMQ;

namespace HistoryService.Infrastructure.RabbitMQ;

public class HistoryRecordProcessor : IEventProcessor
{
    private readonly HistoryServiceDbContext _db;
    private readonly ILogger<HistoryRecordProcessor> _logger;

    public HistoryRecordProcessor(
        HistoryServiceDbContext db,
        ILogger<HistoryRecordProcessor> logger)
    {
        _db = db;
        _logger = logger;
    }

    public async Task ProcessAsync(string message, CancellationToken cancellationToken)
    {
        var dto = JsonSerializer.Deserialize<HistoryRecordDTO>(message);
        if (dto == null)
            return;

        var entity = new HistoryRecord
        {
            HistoryEventType = dto.Type,
            Date = dto.Date,
            Payload = dto.Payload,
            TriggeredBy = dto.TriggeredBy
        };

        _db.HistoryRecords.Add(entity);
        await _db.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Saved history record: {Type}", dto.Type);
    }
}
