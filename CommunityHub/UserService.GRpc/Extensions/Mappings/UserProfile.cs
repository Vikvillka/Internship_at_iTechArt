using AutoMapper;
using UserService.Domain.Entities;

namespace UserService.GRpc.Extensions.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>()
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

        CreateMap<RegisterUserRequest, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore());
    }
}
