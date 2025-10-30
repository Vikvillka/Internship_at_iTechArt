namespace Gateway.API.DTOs.UserDTOs;

public class GatewayCreateUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
