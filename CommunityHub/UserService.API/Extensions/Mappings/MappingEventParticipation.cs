using UserService.Contracts.DTOs.ParticipationDTOs;
using UserService.Domain.Entities;

namespace UserService.API.Extensions.Mappings;

public static class MappingEventParticipation
{
    public static EventParticipationResponse FromEntity(this EventParticipation participation)
    {
        return new EventParticipationResponse
        {
            Id = participation.Id,
            UserId = participation.UserId,
            EventId = participation.EventId,
            IsConfirmed = participation.IsConfirmed
        };
    }
}
