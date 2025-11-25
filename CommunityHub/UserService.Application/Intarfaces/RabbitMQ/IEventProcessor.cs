namespace UserService.Application.Intarfaces.RabbitMQ;

public interface IEventProcessor
{
    Task ProcessAsync(string message, CancellationToken cancellationToken);
}
