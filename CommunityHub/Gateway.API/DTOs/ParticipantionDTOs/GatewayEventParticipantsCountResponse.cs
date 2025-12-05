namespace Gateway.API.DTOs.ParticipantionDTOs;

public class GatewayEventParticipantsCountResponse
{
    public Guid EventId { get; set; }
    public int Count { get; set; }
}
