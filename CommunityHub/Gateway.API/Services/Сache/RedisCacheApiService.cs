using AutoMapper;
using Gateway.API.Interfaces;
using Gateway.API.Interfaces.Cache;
using Gateway.API.Services.Сache.CacheHelpers;
using Google.Protobuf;
using Microsoft.Extensions.Caching.Distributed;
using UserService.GRpc;

namespace Gateway.API.Services.Сache;

public class RedisCacheApiService : IRedisCacheApiService
{
    private readonly IDistributedCache _distributedCache;
    private readonly IUserGrpcClient _apiClient;
    private readonly IMapper _mapper;

    public RedisCacheApiService(IDistributedCache distributedCache, IUserGrpcClient apiClient, IMapper mapper)
    {
        _distributedCache = distributedCache;
        _apiClient = apiClient;
        _mapper = mapper;
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

    public async Task<bool> RemoveUserByIdAsync(Guid id)
    {
        var grpcResponse = await _apiClient.DeleteUserAsync(
            new DeleteUserRequest { UserId = id.ToString() });

        var success = _mapper.Map<bool>(grpcResponse);

        if (success)
        {
            var key = UserCacheKeyHelper.GetRecordByIdKey(id);
            await _distributedCache.RemoveAsync(key);
        }

        return success;
    }
}
