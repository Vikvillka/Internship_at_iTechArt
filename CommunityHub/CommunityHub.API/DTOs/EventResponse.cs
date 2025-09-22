using CommunityHub.API.Models;

namespace CommunityHub.API.DTOs;

public class EventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime EventDate { get; set; }
    public int MaxParticipants { get; set; }
    public string Venue { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public static EventResponse FromModel(Event eventModel)
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