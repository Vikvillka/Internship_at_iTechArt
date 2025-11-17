namespace Gateway.API.DTOs.UserDTOs;

public class GatewayParticipationResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public string EventName { get; set; } = string.Empty;
    public bool IsConfirmed { get; set; }
}
