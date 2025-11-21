using UserService.GRpc;

namespace Gateway.API.Interfaces;

public interface IParticipationGrpcClient
{
    Task<CreateParticipationReply> ParticipateAsync(CreateParticipationRequest request);
    Task<CancelParticipationReply> CancelParticipationAsync(CreateParticipationRequest request);
    Task<GetUserParticipationsReply> GetParticipationsByUserAsync(GetUserParticipationsRequest request);
}
