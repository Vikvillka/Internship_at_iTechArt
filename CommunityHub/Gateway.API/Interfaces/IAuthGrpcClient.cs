using UserService.GRpc;

namespace Gateway.API.Interfaces;

public interface IAuthGrpcClient
{
    Task<GetTokensReply> GetTokensAsync(AuthRequest request);
    Task<RefreshReply> RefreshTokenAsync(RefreshRequest request);
}
