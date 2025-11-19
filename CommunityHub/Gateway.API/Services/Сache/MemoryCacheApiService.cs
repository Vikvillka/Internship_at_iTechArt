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

    public MemoryCacheApiService(IMemoryCache memory, IBestApiClient apiClient)
    {
        _memory = memory;
        _apiClient = apiClient;
    }

    public async Task<GatewayCommunityResponse> GetCommynityByIdAsync(Guid id)
    {
        if(!_memory.TryGetValue(CommunityCacheKeyHelper.GetRecordByIdKey(id), out GatewayCommunityResponse value))
        {
            value = await _apiClient.GetCommunityByIdAsync(id);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(5));

            _memory.Set(CommunityCacheKeyHelper.GetRecordByIdKey(id), value, cacheEntryOptions);
        }
        return value;
    }
}
