using UserService.GRpc;

namespace Gateway.API.Interfaces;

public interface IUserGrpcClient
{
    Task<RegisterUserReply> RegisterUserAsync(RegisterUserRequest request);
    Task<DeleteUserReply> DeleteUserAsync(DeleteUserRequest request);
    Task<GetUserReply> GetUserByIdAsync(GetUserByIdRequest request);
    Task<GetUsersReply> GetAllUsersAsync(Google.Protobuf.WellKnownTypes.Empty request);
}
