using CommunityHub.Contracts.DTOs.CommunitiesDTOs;
using CommunityHub.Domain.Common;
using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Interfaces.Repositories;

public interface ICommunityRepository : IRepository<Community>
{
    Task<IList<Community>> SearchAsync(string? category, string? city, string? country);
    Task<PagedResult<Community>> PagedSearchAsync(CommunitySearchRequest request);
}