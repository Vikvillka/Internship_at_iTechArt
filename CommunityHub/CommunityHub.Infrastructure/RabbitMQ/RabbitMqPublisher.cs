using CommunityHub.Application.Interfaces.RabbitMQ;
using HistoryService.Contracts.DeleteEntityDTOs;
using HistoryService.Contracts.HistoryRecordDTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace CommunityHub.Infrastructure.RabbitMQ;

public class RabbitMqPublisher : IRabbitMqPublisher
{
    private readonly RabbitMqSettings _settings;
    private readonly ConnectionFactory _factory;
    private readonly ILogger<RabbitMqPublisher> _logger;

    public RabbitMqPublisher(IOptions<RabbitMqSettings> options, ILogger<RabbitMqPublisher> logger)
    {
        _settings = options.Value;

        _factory = new ConnectionFactory
        {
            HostName = _settings.Host,
            UserName = _settings.Username,
            Password = _settings.Password
        };
        _logger = logger;
    }

    public async Task PublishDeleteEntityAsync(DeleteEntityDTO dto)
    {
        await PublishAsync(_settings.DeleteEntityExchange, dto);
        _logger.LogInformation(
            "Published deletion message: EntityId={EntityId}, EntityType={EntityType}",
            dto.EntityId,
            dto.EntityType
        );
    }


    public async Task PublishUpdateEntityAsync(HistoryRecordDTO dto)
    {
        await PublishAsync(_settings.HistoryExchange, dto); 
        _logger.LogInformation(
            "Published update message: Type={Type}, Payload={Payload}",
            dto.Type,
            dto.Payload
        );
    }

    private async Task PublishAsync<T>(string exchange, T dto)
    {
        await using var connection = await _factory.CreateConnectionAsync();
        await using var channel = await connection.CreateChannelAsync();

        var json = JsonSerializer.Serialize(dto);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(
            exchange: exchange,
            routingKey: "",
            body: body
        );
    }
}
