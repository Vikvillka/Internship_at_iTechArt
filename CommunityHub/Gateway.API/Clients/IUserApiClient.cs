using Refit;
using Gateway.API.DTOs.AuthDTOs;

namespace Gateway.API.Clients;

public interface IUserApiClient
{
    [Post("/auth/validateBasic")]
    Task<ApiResponse<object>> ValidateBasicAsync([Body] GatewayAuthRequest request);
}
