using Refit;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;

namespace Gateway.API.Clients;

public interface IMyBestApi
{
    private const string CommunityPath = "/community";

    #region Community
    [Get(CommunityPath + "/getAll")]
    Task<List<CommunityResponse>> GetAllCommunitiesAsync();

    [Get(CommunityPath + "/get/{id}")]
    Task<CommunityResponse> GetCommunityByIdAsync(Guid id);

    [Post(CommunityPath + "/create")]
    Task<CommunityResponse> CreateCommunityAsync([Body] CreateCommunityRequest request);

    [Put(CommunityPath + "/update")]
    Task<CommunityResponse> UpdateCommunityAsync([Body] UpdateCommunityRequest request);

    [Delete(CommunityPath + "/delete/{id}")]
    Task<CommunityResponse> DeleteCommunityByIdAsync(Guid id);
    #endregion
}
