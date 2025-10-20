using CommunityHub.Contracts.DTOs.AuthDTOs;
using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Services;

public interface IJwtService
{
    TokenResponse GenerateTokens(User user);
    TokenResponse Refresh(string refreshToken);
}
