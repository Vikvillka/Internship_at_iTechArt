namespace CommunityHub.Contracts.DTOs.UserDTOs;

public class CreateUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
