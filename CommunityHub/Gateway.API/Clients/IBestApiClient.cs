using Refit;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Contracts.DTOs.Enums;
using Gateway.API.DTOs.CommunitiesDTOs;
using Gateway.API.DTOs.TagDTOs;
using Gateway.API.DTOs.EventDTOs;
using CommunityHub.Contracts.DTOs.EventDTOs;


namespace Gateway.API.Clients;

public interface IBestApiClient
{
    const string CommunityPath = "/community";
    const string EventPath = "/event";
    const string UserPath = "/user";
    const string TagPath = "/tag";
    const string AuthPath = "/auth";

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
    Task DeleteCommunityByIdAsync(Guid id);
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

    #region Tag
    [Get(TagPath + "/getAll")]
    Task<List<GatewayTagResponse>> GetAllTagsAsync();
    #endregion
}
