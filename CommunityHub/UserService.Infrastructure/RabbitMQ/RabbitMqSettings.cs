namespace UserService.Infrastructure.RabbitMQ;

public class RabbitMqSettings
{
    public string Host { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public string DeleteEntityQueue { get; set; } = "OnMyEntityDeleted";
    public string DeleteEntityExchange { get; set; } = "DeleteEntity";
}
