using UserService.GRpc;

namespace Gateway.API.Interfaces.Cache;

public interface IRedisCacheApiService
{
    Task<GetUserReply> GetUserByIdAsync(Guid id);
    Task<bool> RemoveUserByIdAsync(Guid id);
}
