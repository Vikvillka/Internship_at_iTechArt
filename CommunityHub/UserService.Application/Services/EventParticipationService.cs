//using UserService.Application.Intarfaces.Repositories;
//using UserService.Application.Intarfaces.Services;
//using UserService.Domain.Entities;

//namespace UserService.Application.Services;

//public class EventParticipationService : IEventParticipationService
//{
//    private readonly IEventParticipationRepository _eventParticipationRepository;
//    private readonly IUserService _userService;

//    public EventParticipationService(IEventParticipationRepository eventParticipationRepository, IUserService userService)
//    {
//        _eventParticipationRepository = eventParticipationRepository;
//        _userService = userService;
//    }

//    public async Task<IList<EventParticipation>> GetParticipationsByUserAsync(Guid userId)
//    {
//        await _userService.GetByUserIdAsync(userId);
//        return await _eventParticipationRepository.GetByUserAsync(userId);
//    }
//}
