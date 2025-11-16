using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

using UserService.Application.Intarfaces.Services;
using UserService.Domain.Entities;

namespace UserService.GRpc.Services;

public class UserServiceImpl : UserService.UserServiceBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserServiceImpl(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public override async Task<RegisterUserReply> RegisterUser(RegisterUserRequest request, ServerCallContext context)
    {
        var userEntity = _mapper.Map<User>(request);
        var createdUser = await _userService.RegisterAsync(userEntity, request.Password);
        return new RegisterUserReply
        {
            User = _mapper.Map<UserModel>(createdUser)
        };
    }

    public override async Task<DeleteUserReply> DeleteUser(DeleteUserRequest request, ServerCallContext context)
    {
        var success = await _userService.DeleteAsync(Guid.Parse(request.UserId));
        return new DeleteUserReply { Success = success };
    }

    public override async Task<GetUserReply> GetUserById(GetUserByIdRequest request, ServerCallContext context)
    {
        var user = await _userService.GetByUserIdAsync(Guid.Parse(request.UserId));
        return new GetUserReply { User = _mapper.Map<UserModel>(user) };
    }

    public override async Task<GetUsersReply> GetAllUsers(Empty request, ServerCallContext context)
    {
        var users = await _userService.GetAllAsync();
        var usersList = new UsersList();
        usersList.Users.AddRange(users.Select(u => _mapper.Map<UserModel>(u)));
        return new GetUsersReply { UsersList = usersList };
    }
}
