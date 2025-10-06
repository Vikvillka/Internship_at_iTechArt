using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;

namespace CommunityHub.Application.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public async Task<IList<EventTag>> GetByIdsAsync(List<Guid> tagIds)
    {
        return await _tagRepository.GetByIdsAsync(tagIds);
    }

    public async Task<IList<EventTag>> GetAllAsync()
    {
        return await _tagRepository.GetAllAsync();
    }
}

