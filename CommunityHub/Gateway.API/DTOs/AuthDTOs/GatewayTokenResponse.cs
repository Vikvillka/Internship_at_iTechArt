namespace Gateway.API.DTOs.AuthDTOs;

public class GatewayTokenResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
