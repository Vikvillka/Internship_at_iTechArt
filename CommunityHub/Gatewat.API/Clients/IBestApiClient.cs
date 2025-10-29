using Refit;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Contracts.DTOs.Enums;
using Gateway.API.DTOs.CommunitiesDTOs;
using Gateway.API.DTOs.AuthDTOs;
using Gateway.API.DTOs.TagDTOs;
using Gateway.API.DTOs.EventDTOs;
using CommunityHub.Contracts.DTOs.EventDTOs;
using Gateway.API.DTOs.UserDTOs;
using CommunityHub.Contracts.DTOs.UserDTOs;
using CommunityHub.Contracts.DTOs.AuthDTOs;

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
    Task<List<GatewayCommunityResponse>> GetAllCommunitiesAsync();

    [Get(CommunityPath + "/get/{id}")]
    Task<GatewayCommunityResponse> GetCommunityByIdAsync(Guid id);

    [Post(CommunityPath + "/create")]
    Task<GatewayCommunityResponse> CreateCommunityAsync([Body] CreateCommunityRequest request);

    [Put(CommunityPath + "/update")]
    Task<GatewayCommunityResponse> UpdateCommunityAsync([Body] UpdateCommunityRequest request);

    [Delete(CommunityPath + "/delete/{id}")]
    Task<GatewayCommunityResponse> DeleteCommunityByIdAsync(Guid id);
    #endregion

    #region Event
    [Get(EventPath + "/getAll")]
    Task<List<GatewayEventResponse>> GetAllPlannedEventsAsync();

    [Get(EventPath + "/get/{id}")]
    Task<GatewayEventResponse> GetEventByIdAsync(Guid id);

    [Get(EventPath + "/getAllByCommunity/{communityId}")]
    Task<List<GatewayEventResponse>> GetEventsByCommunityIdAsync(Guid communityId);

    [Post(EventPath + "/create")]
    Task<GatewayEventResponse> CreateEventAsync([Body] CreateEventRequest request);

    [Put(EventPath + "/update")]
    Task<GatewayEventResponse> UpdateEventAsync([Body] UpdateEventRequest request);

    [Patch(EventPath + "/{id}/status")]
    Task<GatewayEventResponse> UpdateEventStatusAsync(Guid id, [Body] EventStatusDto newStatus);
    #endregion

    #region User
    [Post(UserPath)]
    Task<GatewayUserResponse> RegisterUserAsync([Body] CreateUserRequest request);

    [Delete(UserPath + "/{id}")]
    Task DeleteUserByIdAsync(Guid id);
    #endregion

    #region Tag
    [Get(TagPath + "/getAll")]
    Task<List<GatewayTagResponse>> GetAllTagsAsync();
    #endregion

    #region Auth
    [Post(AuthPath + "/getTokens")]
    Task<GatewayTokenResponse> GetTokensAsync([Body] AuthRequest request);

    [Post(AuthPath + "/refresh")]
    Task<GatewayTokenResponse> RefreshTokenAsync([Body] RefreshRequest request);
    #endregion
}
