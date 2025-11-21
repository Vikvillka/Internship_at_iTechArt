using Microsoft.Extensions.Caching.Distributed;

using UserService.Application.Intarfaces.Services.Cache;

namespace UserService.Infrastructure.Services.Cache;

public class UserCacheService : IUserCacheService
{
    private readonly IDistributedCache _cache;

    public UserCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task RemoveUserByIdAsync(Guid userId)
    {
        var key = $"User:{userId}";
        await _cache.RemoveAsync(key);
    }
}
