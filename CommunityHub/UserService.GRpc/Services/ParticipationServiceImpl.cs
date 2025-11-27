using AutoMapper;
using Grpc.Core;
using UserService.Application.Intarfaces.Services;

namespace UserService.GRpc.Server.Services;

public class ParticipationServiceImpl : ParticipationService.ParticipationServiceBase
{
    private readonly IEventParticipationService _participationService;
    private readonly IMapper _mapper;

    public ParticipationServiceImpl(IEventParticipationService participationService, IMapper mapper)
    {
        _participationService = participationService;
        _mapper = mapper;
    }

    public override async Task<CreateParticipationReply> Participate(CreateParticipationRequest request, ServerCallContext context)
    {
        var participation = await _participationService.ParticipateAsync(Guid.Parse(request.UserId), Guid.Parse(request.EventId));

        return new CreateParticipationReply
        {
            Participation = _mapper.Map<ParticipationModel>(participation)
        };
    }

    public override async Task<CancelParticipationReply> CancelParticipation(CreateParticipationRequest request, ServerCallContext context)
    {
        var participation = await _participationService.CancelParticipationAsync(
            Guid.Parse(request.UserId),
            Guid.Parse(request.EventId)
        );

        return new CancelParticipationReply
        {
            Success = !participation.IsConfirmed
        };
    }

    public override async Task<GetUserParticipationsReply> GetParticipationsByUser(GetUserParticipationsRequest request, ServerCallContext context)
    {
        var participations = await _participationService.GetParticipationsByUserAsync(
            Guid.Parse(request.UserId)
        );

        var list = new ParticipationsList();
        list.Items.AddRange(participations.Select(p => _mapper.Map<ParticipationModel>(p)));

        return new GetUserParticipationsReply
        {
            Participations = list
        };
    }
}
