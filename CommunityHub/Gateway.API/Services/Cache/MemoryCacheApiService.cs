using Microsoft.Extensions.Caching.Memory;

using Gateway.API.Clients;
using Gateway.API.DTOs.CommunitiesDTOs;
using Gateway.API.Interfaces.Cache;
using Gateway.API.Services.Сache.CacheHelpers;

namespace Gateway.API.Services.Сache;

public class MemoryCacheApiService : IMemoryCacheApiService
{
    private readonly IMemoryCache _memory;
    private readonly IBestApiClient _apiClient;
    private readonly IConfiguration _config;

    public MemoryCacheApiService(IMemoryCache memory, IBestApiClient apiClient, IConfiguration config)
    {
        _memory = memory;
        _apiClient = apiClient;
        _config = config;
    }

    public async Task<GatewayCommunityResponse> GetCommunityByIdAsync(Guid id)
    {
        if(!_memory.TryGetValue(CommunityCacheKeyHelper.GetRecordByIdKey(id), out GatewayCommunityResponse value))
        {
            value = await _apiClient.GetCommunityByIdAsync(id);
            
            var settingTime = _config.GetValue<int>("CacheSettings:CommunityCacheSeconds");
            
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(settingTime));

            _memory.Set(CommunityCacheKeyHelper.GetRecordByIdKey(id), value, cacheEntryOptions);
        }
        return value;
    }
}
