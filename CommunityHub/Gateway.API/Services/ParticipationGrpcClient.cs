using Gateway.API.Interfaces;
using UserService.GRpc;

namespace Gateway.API.Services;

public class ParticipationGrpcClient : IParticipationGrpcClient
{
    private readonly ParticipationService.ParticipationServiceClient _client;

    public ParticipationGrpcClient(ParticipationService.ParticipationServiceClient client)
    {
        _client = client;
    }

    public async Task<CreateParticipationReply> ParticipateAsync(CreateParticipationRequest request)
    {
        return await _client.ParticipateAsync(request);
    }

    public async Task<CancelParticipationReply> CancelParticipationAsync(CreateParticipationRequest request)
    {
        return await _client.CancelParticipationAsync(request);
    }

    public async Task<GetUserParticipationsReply> GetParticipationsByUserAsync(GetUserParticipationsRequest request)
    {
        return await _client.GetParticipationsByUserAsync(request);
    }
}
