using Refit;

using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Contracts.DTOs.Enums;
using Gateway.API.DTOs.CommunitiesDTOs;
using Gateway.API.DTOs.AuthDTOs;
using Gateway.API.DTOs.TagDTOs;
using Gateway.API.DTOs.EventDTOs;
using CommunityHub.Contracts.DTOs.EventDTOs;
using Gateway.API.DTOs.UserDTOs;
using Gateway.API.DTOs.ImageDTOs;

namespace Gateway.API.Clients;

public interface IBestApiClient
{
    const string CommunityPath = "/community";
    const string EventPath = "/event";
    const string UserPath = "/user";
    const string TagPath = "/tag";
    const string AuthPath = "/auth";
    const string ImagePath = "/image";

    #region Community
    [Get(CommunityPath + "/getAll")]
    Task<List<GatewayCommunityResponse>> GetAllCommunitiesAsync();

    [Get(CommunityPath + "/get/{id}")]
    Task<GatewayCommunityResponse> GetCommunityByIdAsync(Guid id);

    [Post(CommunityPath + "/create")]
    Task<GatewayCommunityResponse> CreateCommunityAsync([Body] CreateCommunityRequest request);

    [Put(CommunityPath + "/update")]
    Task UpdateCommunityAsync([Body] UpdateCommunityRequest request);

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
    Task UpdateEventAsync([Body] UpdateEventRequest request);

    [Patch(EventPath + "/{id}/status")]
    Task UpdateEventStatusAsync(Guid id, [Body] EventStatusDto newStatus);
    #endregion

    #region Image
    [Multipart]
    [Post(ImagePath + "/upload")]
    Task<GatewayImageUploadResponse> UploadImageAsync([AliasAs("file")] StreamPart file);

    [Get(ImagePath + "/{fileName}")]
    Task<ApiResponse<HttpContent>> GetImageAsync(string fileName);

    [Delete(ImagePath + "/delete/{fileName}")]
    Task DeleteImageAsync(string fileName);
    #endregion

    #region Tag
    [Get(TagPath + "/getAll")]
    Task<List<GatewayTagResponse>> GetAllTagsAsync();
    #endregion
}
