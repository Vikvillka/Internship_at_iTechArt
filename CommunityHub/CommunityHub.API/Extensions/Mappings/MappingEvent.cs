using CommunityHub.API.DTOs.EventDTOs;
using CommunityHub.API.Models;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingEvent
{
    public static EventResponse FromModel(this Event eventModel)
    {
        return new EventResponse
        {
            Id = eventModel.Id,
            Title = eventModel.Title,
            Description = eventModel.Description,
            EventDate = eventModel.EventDate,
            MaxParticipants = eventModel.MaxParticipants,
            Venue = eventModel.Venue,
            Address = eventModel.Address,
            Status = eventModel.Status.ToString()
        };
    }
}

