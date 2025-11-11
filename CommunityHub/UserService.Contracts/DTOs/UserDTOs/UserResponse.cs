using UserService.Contracts.DTOs.Enums;

namespace UserService.Contracts.DTOs.UserDTOs;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public GenderDTOs Gender { get; set; }
    public string City { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
}
