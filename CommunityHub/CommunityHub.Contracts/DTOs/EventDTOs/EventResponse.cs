using CommunityHub.Contracts.DTOs.TagDTOs;

namespace CommunityHub.Contracts.DTOs.EventDTOs;

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
    public int Duration { get; set; }  
    public string? ImagePath { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }
    public List<TagResponse> Tags { get; set; } = [];
}