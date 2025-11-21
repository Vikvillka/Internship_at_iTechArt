namespace UserService.Contracts.DTOs.ParticipationDTOs;

public class EventParticipationResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid EventId { get; set; }
    public bool IsConfirmed { get; set; }
}
