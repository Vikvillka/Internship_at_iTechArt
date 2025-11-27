using Gateway.API.DTOs.CommunitiesDTOs;

namespace Gateway.API.Interfaces.Cache;

public interface IMemoryCacheApiService
{
    Task<GatewayCommunityResponse> GetCommunityByIdAsync(Guid id);
}
