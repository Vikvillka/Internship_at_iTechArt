using UserService.Application.Intarfaces.Repositories;
using UserService.Application.Intarfaces.Services;
using UserService.Domain.Entities;
using UserService.Domain.Exceptions;

namespace UserService.Application.Services;

public class CommunitySubscriptionService : ICommunitySubscriptionService
{
    private readonly ICommunitySubscriptionRepository _repository;
    private readonly IUserService _userService;

    public CommunitySubscriptionService(ICommunitySubscriptionRepository repository, IUserService userService)
    {
        _repository = repository;
        _userService = userService;
    }

    public async Task<IList<CommunitySubscription>> GetSubscriptionsByUserAsync(Guid userId)
    {
        await _userService.GetByUserIdAsync(userId);

        return await _repository.GetByUserAsync(userId);
    }

    public async Task<CommunitySubscription> SubscribeAsync(Guid userId, Guid communityId)
    {
        await _userService.GetByUserIdAsync(userId);

        var existing = await _repository.GetByUserAndCommunityAsync(userId, communityId);

        if (existing != null)
        {
            if (existing.IsActive)
                throw new ConflictException("Conflict", "Already subscribed");

            await _repository.UpdateStatusAsync(existing.Id, true);
            existing.IsActive = true;
            return existing;
        }

        var subscription = new CommunitySubscription
        {
            UserId = userId,
            CommunityId = communityId
        };

        await _repository.CreateAsync(subscription);
        return subscription;
    }

    public async Task<CommunitySubscription> UnsubscribeAsync(Guid userId, Guid communityId)
    {
        await _userService.GetByUserIdAsync(userId);

        var existing = await _repository.GetByUserAndCommunityAsync(userId, communityId);

        if (existing == null || !existing.IsActive)
            throw new NotFoundException("NotFound", "Subscription not found");

        await _repository.UpdateStatusAsync(existing.Id, false);
        existing.IsActive = false;
        return existing;
    }
}

