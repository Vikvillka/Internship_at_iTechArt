using Google.Protobuf;
using Microsoft.Extensions.Caching.Distributed;
using UserService.GRpc;

using Gateway.API.Interfaces;
using Gateway.API.Interfaces.Cache;
using Gateway.API.Services.Сache.CacheHelpers;

namespace Gateway.API.Services.Сache;

public class RedisCacheApiService : IRedisCacheApiService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IUserGrpcClient _apiClient;

    public RedisCacheApiService(IDistributedCache distributedCache, IUserGrpcClient apiClient)
    {
        _distributedCache = distributedCache;
        _apiClient = apiClient;
    }

    public async Task<GetUserReply> GetUserByIdAsync(Guid id)
    {
        var key = UserCacheKeyHelper.GetRecordByIdKey(id);
        
        var bytes = await _distributedCache.GetAsync(key);
        
        GetUserReply cachedValue;

        if (bytes is null)
        {
            cachedValue = await _apiClient.GetUserByIdAsync(
                new GetUserByIdRequest { UserId = id.ToString() });
            await _distributedCache.SetAsync(
                key,
                cachedValue.ToByteArray(),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(5)
                });
        }
        else
        {
            cachedValue = GetUserReply.Parser.ParseFrom(bytes);
        }

        return cachedValue;
    }
}
