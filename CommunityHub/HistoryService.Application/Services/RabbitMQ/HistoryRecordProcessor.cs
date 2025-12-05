using Microsoft.Extensions.Logging;
using System.Text.Json;

using HistoryService.Domain.Entities;
using HistoryService.Application.Intefaces.RabbitMQ;
using HistoryService.Application.Intefaces.Repositories;
using HistoryService.Contracts.HistoryRecordDTOs;

namespace HistoryService.Application.Services.RabbitMQ;

public class HistoryRecordProcessor : IEventProcessor
{
    private readonly IHistoryRecordRepository _repository;
    private readonly ILogger<HistoryRecordProcessor> _logger;

    public HistoryRecordProcessor(
        IHistoryRecordRepository repository,
        ILogger<HistoryRecordProcessor> logger)
    {
        _repository = repository;
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

        await _repository.CreateAsync(entity);
        _logger.LogInformation("Saved history record: {Type}", dto.Type);
    }
}
