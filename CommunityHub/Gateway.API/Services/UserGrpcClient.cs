using Gateway.API.Interfaces;
using UserService.GRpc;

namespace Gateway.API.Services;

public class UserGrpcClient : IUserGrpcClient
{
    private readonly UserService.GRpc.UserService.UserServiceClient _client;

    public UserGrpcClient(UserService.GRpc.UserService.UserServiceClient client)
    {
        _client = client;
    }

    public async Task<RegisterUserReply> RegisterUserAsync(RegisterUserRequest request)
    {
        return await _client.RegisterUserAsync(request);
    }

    public async Task<DeleteUserReply> DeleteUserAsync(DeleteUserRequest request)
    {
        return await _client.DeleteUserAsync(request);
    }

    public async Task<GetUserReply> GetUserByIdAsync(GetUserByIdRequest request)
    {
        return await _client.GetUserByIdAsync(request);
    }

    public async Task<GetUsersReply> GetAllUsersAsync(Google.Protobuf.WellKnownTypes.Empty request)
    {
        return await _client.GetAllUsersAsync(request);
    }
}
