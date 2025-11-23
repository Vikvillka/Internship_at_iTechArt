using CommunityHub.Application.Interfaces.RabbitMQ;
using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;
using CommunityHub.Domain.Exceptions;
using HistoryService.Contracts.DeleteEntityDTOs;

namespace CommunityHub.Application.Services;

public class CommunityService : ICommunityService
{
    private readonly ICommunityRepository _communityRepository;
    private readonly IRabbitMqPublisher _publisher;

    public CommunityService(ICommunityRepository communityRepository, IRabbitMqPublisher publisher)
    {
        _communityRepository = communityRepository;
        _publisher = publisher;
    }

    public async Task<IList<Community>> GetAllAsync()
    {
        return await _communityRepository.GetAllAsync();
    }

    public async Task<Community?> GetByIdAsync(Guid id)
    { 
        var community = await _communityRepository.GetByIdAsync(id);
        if (community == null)
            throw new NotFoundException("NotFound", $"Community with id '{id}' not found");

        return community;
    }

    public async Task<Community> CreateAsync(Community community)
    {
        await EnsureUniqueCommunityNameAsync(community);
        return await _communityRepository.CreateAsync(community);
    }

    public async Task<bool> UpdateAsync(Community community)
    {
        var existingCommunity = await _communityRepository.GetByIdAsync(community.Id);
        if (existingCommunity == null) 
            throw new NotFoundException("NotFound", $"Community with id '{community.Id}' not found");

        await EnsureUniqueCommunityNameAsync(community, community.Id);
        return await _communityRepository.UpdateAsync(community);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingCommunity = await _communityRepository.GetByIdAsync(id);
        if (existingCommunity == null) 
            throw new NotFoundException("NotFound", $"Community with id '{id}' not found");
        
        var result = await _communityRepository.DeleteAsync(id);
        if (result)
        {
            await _publisher.PublishDeleteEntityAsync(
                new DeleteEntityDTO
                {
                    EntityId = id,
                    EntityType = "Community"
                });
        }
        return result; 
    }

    public async Task<IList<Community>> SearchAsync(string? category, string? city, string? country)
    {
        return await _communityRepository.SearchAsync(category, city, country);
    }

    private async Task EnsureUniqueCommunityNameAsync(Community community, Guid? excludeId = null)
    {
        var existing = await _communityRepository.SearchAsync(
            category: null,
            city: community.City,
            country: community.Country
        );

        if (existing.Any(c => (c.Id != excludeId) &&
            c.Name.Equals(community.Name, StringComparison.OrdinalIgnoreCase)))
        {
            throw new ConflictException("Conflict", $"Community with name '{community.Name}' is already taken");
        }
    }
}
