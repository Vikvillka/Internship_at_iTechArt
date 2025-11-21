namespace Gateway.API.DTOs.ParticipantionDTOs;

public class GatewayCreateParticipationRequest
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
}
