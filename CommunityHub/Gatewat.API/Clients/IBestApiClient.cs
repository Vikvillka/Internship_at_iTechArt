using Refit;

using CommunityHub.Contracts.DTOs.AuthDTOs;
using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Contracts.DTOs.Enums;
using CommunityHub.Contracts.DTOs.EventDTOs;
using CommunityHub.Contracts.DTOs.TagDTOs;
using CommunityHub.Contracts.DTOs.UserDTOs;

namespace Gateway.API.Clients;

public interface IBestApiClient
{
    private const string CommunityPath = "/community";
    private const string EventPath = "/event";
    private const string UserPath = "/user";
    private const string TagPath = "/tag";
    private const string AuthPath = "/auth";

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

    #region Event
    [Get(EventPath + "/getAll")]
    Task<List<EventResponse>> GetAllPlannedEventsAsync();

    [Get(EventPath + "/get/{id}")]
    Task<EventResponse> GetEventByIdAsync(Guid id);

    [Get(EventPath + "/getAllByCommunity/{communityId}")]
    Task<List<EventResponse>> GetEventsByCommunityIdAsync(Guid communityId);

    [Post(EventPath + "/create")]
    Task<EventResponse> CreateEventAsync([Body] CreateEventRequest request);

    [Put(EventPath + "/update")]
    Task<EventResponse> UpdateEventAsync([Body] UpdateEventRequest request);

    [Patch(EventPath + "/{id}/status")]
    Task<EventResponse> UpdateEventStatusAsync(Guid id, [Body] EventStatusDto newStatus);
    #endregion

    #region User
    [Post(UserPath)]
    Task<UserResponse> RegisterUserAsync([Body] CreateUserRequest request);

    [Delete(UserPath + "/{id}")]
    Task DeleteUserByIdAsync(Guid id);
    #endregion

    #region Tag
    [Get(TagPath + "/getAll")]
    Task<List<TagResponse>> GetAllTagsAsync();
    #endregion

    #region Auth
    [Post(AuthPath + "/getTokens")]
    Task<TokenResponse> GetTokensAsync([Body] AuthRequest request);

    [Post(AuthPath + "/refresh")]
    Task<TokenResponse> RefreshTokenAsync([Body] RefreshRequest request);
    #endregion
}
