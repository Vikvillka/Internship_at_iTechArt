using HistoryService.Contracts.DeleteEntityDTOs;

namespace CommunityHub.Application.Interfaces.RabbitMQ;

public interface IRabbitMqPublisher
{
    Task PublishDeleteEntityAsync(DeleteEntityDTO dto);
}
