using AutoMapper;

using CommunityHub.Contracts.DTOs.ImageDTOs;
using Gateway.API.DTOs.ImageDTOs;

namespace Gateway.API.Extensions.Mappings.Profiles;

public class GatewayImageProfile : Profile
{
    public GatewayImageProfile()
    {
        CreateMap<ImageUploadResponse, GatewayImageUploadResponse>();
    }
}
