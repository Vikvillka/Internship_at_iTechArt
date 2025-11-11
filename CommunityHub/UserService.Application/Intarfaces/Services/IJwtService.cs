using UserService.Contracts.DTOs.AuthDTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Intarfaces.Services;

public interface IJwtService
{
    TokenResponse GenerateTokens(User user);
    TokenResponse Refresh(string refreshToken);
}
