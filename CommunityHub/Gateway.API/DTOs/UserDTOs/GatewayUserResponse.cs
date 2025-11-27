namespace Gateway.API.DTOs.UserDTOs;

public class GatewayUserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty; 
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
