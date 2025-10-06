using AutoMapper;

using CommunityHub.API.DTOs.CommunitiesDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.API.DTOs.Profiles;

public class CommunityProfile : Profile
{
    public CommunityProfile()
    {
        CreateMap<Community, CommunityResponse>();
        CreateMap<CreateCommunityRequest, Community>();
        CreateMap<UpdateCommunityRequest, Community>();
    }
}