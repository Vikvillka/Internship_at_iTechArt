namespace HistoryService.Application.Intefaces.RabbitMQ;

public interface IEventProcessor
{
    Task ProcessAsync(string message, CancellationToken cancellationToken);
}
