namespace UserService.Contracts.DTOs.ParticipationDTOs;

public class CreateEventParticipationRequest
{
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
}
