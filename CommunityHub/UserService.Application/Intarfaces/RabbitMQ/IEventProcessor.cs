namespace UserService.Application.Intarfaces.RabbitMQ;

public interface IEventProcessor
{
    //string QueueName { get; }
    Task ProcessAsync(string message, CancellationToken cancellationToken);
}
