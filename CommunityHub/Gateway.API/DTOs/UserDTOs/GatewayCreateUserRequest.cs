using UserService.Contracts.DTOs.Enums;

namespace Gateway.API.DTOs.UserDTOs;

public class GatewayCreateUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public GenderDTOs Gender { get; set; }    
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
