using CommunityHub.Application.Interfaces.Repositories;
using CommunityHub.Application.Interfaces.Services;
using CommunityHub.Domain.Entities;
using System.Reflection.Metadata.Ecma335;

namespace CommunityHub.Application.Services;

public class TagService : ITagService
{
    private readonly ITagRepository _tagRepository;

    public TagService(ITagRepository tagRepository)
    {
        _tagRepository = tagRepository;
    }

    public Task<List<EventTag>> GetByIdsAsync(List<Guid> tagIds)
    {
        return _tagRepository.GetByIdsAsync(tagIds);
    }

    public Task<List<EventTag>> GetAllAsync()
    {
        return _tagRepository.GetAllAsync();
    }
}

