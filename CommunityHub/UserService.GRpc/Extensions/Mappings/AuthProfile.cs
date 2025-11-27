using AutoMapper;
using UserService.Contracts.DTOs.AuthDTOs;

namespace UserService.GRpc.Extensions.Mappings
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<TokenResponse, TokenModel>();
        }
    }
}
