using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Domain.Common;
using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Services;

public interface ICommunityService
{
    Task<IList<Community>> GetAllAsync();
    Task<Community?> GetByIdAsync(Guid id);
    Task<Community> CreateAsync(Community community);
    Task<bool> UpdateAsync(Community community);
    Task<bool> DeleteAsync(Guid id);
    Task<PagedResult<Community>> GetCommunitiesBySearchAsync(CommunitySearchRequest request);
    Task<IList<Community>> GetCommunitiesByUserIdAsync(Guid userId);
}

