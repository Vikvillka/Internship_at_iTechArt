namespace UserService.Application.Intarfaces.Services.Cache;

public interface IUserCacheService
{
    Task RemoveUserByIdAsync(Guid userId);
}
