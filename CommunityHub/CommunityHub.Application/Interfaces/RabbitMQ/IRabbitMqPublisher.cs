using HistoryService.Contracts.DeleteEntityDTOs;
using HistoryService.Contracts.HistoryRecordDTOs;

namespace CommunityHub.Application.Interfaces.RabbitMQ;

public interface IRabbitMqPublisher
{
    Task PublishDeleteEntityAsync(DeleteEntityDTO dto);
    Task PublishUpdateEntityAsync(HistoryRecordDTO dto);
}
