using Refit;

using CommunityHub.Contracts.DTOs.AuthDTOs;

namespace Gateway.API.Clients;

public interface IUserApiClient
{
    [Post("/auth/validateBasic")]
    Task<ApiResponse<object>> ValidateBasicAsync([Body] AuthRequest request);
}
