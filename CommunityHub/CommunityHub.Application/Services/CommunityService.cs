using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Services
{
    public class CommunityService : ICommunityService
    {
        private readonly ICommunityRepository _communityRepository;

        public CommunityService(ICommunityRepository communityRepository)
        {
            _communityRepository = communityRepository;
        }

        public async Task<IList<Community>> GetAllAsync()
        {
            return await _communityRepository.GetAllAsync();
        }

        public async Task<Community?> GetByIdAsync(Guid id)
        {
            return await _communityRepository.GetByIdAsync(id);
        }

        public async Task<Community> CreateAsync(Community community)
        {
            return await _communityRepository.CreateAsync(community);
        }

        public async Task<bool> UpdateAsync(Community community)
        {
            var existingCommunity = await _communityRepository.GetByIdAsync(community.Id);
            if (existingCommunity == null) return false;
            
            return await _communityRepository.UpdateAsync(community);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingCommunity = await _communityRepository.GetByIdAsync(id);
            if (existingCommunity == null) return false;

            return await _communityRepository.DeleteAsync(id);
        }

        public async Task<IList<Community>> SearchAsync(string? category, string? city, string? country)
        {
            return await _communityRepository.SearchAsync(category, city, country);
        }
    }
}
