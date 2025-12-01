using CommunityHub.Contracts.DTOs.EventDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.Extensions.Mappings;

public static class MappingEvent
{
    public static EventResponse FromEntity(this Event eventModel)
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
            Status = eventModel.Status.ToString(),
            Duration = eventModel.Duration,
            ImagePath = eventModel.ImagePath,
            Latitude = eventModel.Latitude,
            Longitude = eventModel.Longitude,
            Tags = eventModel.Tags.Select(t => t.FromEntity()).ToList()
        };
    }
}

