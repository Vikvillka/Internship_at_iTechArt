using Gateway.API.Interfaces;
using UserService.GRpc;

namespace Gateway.API.Services;

public class AuthGrpcClient : IAuthGrpcClient
{
    private readonly AuthService.AuthServiceClient _client;

    public AuthGrpcClient(AuthService.AuthServiceClient client)
    {
        _client = client;
    }

    public async Task<GetTokensReply> GetTokensAsync(AuthRequest request)
    {
        return await _client.GetTokensAsync(request);
    }

    public async Task<RefreshReply> RefreshTokenAsync(RefreshRequest request)
    {
        return await _client.RefreshTokenAsync(request);
    }
}

