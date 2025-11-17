using AutoMapper;
using Grpc.Core;
using UserService.Application.Intarfaces.Services;

namespace UserService.GRpc.Server.Services;

public class AuthServiceImpl : AuthService.AuthServiceBase
{
    private readonly IUserService _userService;
    private readonly IJwtService _jwtService;
    private readonly IMapper _mapper;

    public AuthServiceImpl(IUserService userService, IJwtService jwtService, IMapper mapper)
    {
        _userService = userService;
        _jwtService = jwtService;
        _mapper = mapper;
    }

    public override async Task<GetTokensReply> GetTokens(AuthRequest request, ServerCallContext context)
    {
        var user = await _userService.AuthenticateAsync(request.Username, request.Password);
        var tokensDto = _jwtService.GenerateTokens(user);

        return new GetTokensReply
        {
            Tokens = _mapper.Map<TokenModel>(tokensDto)
        };
    }

    public override Task<RefreshReply> RefreshToken(RefreshRequest request, ServerCallContext context)
    {
        var newTokens = _jwtService.Refresh(request.RefreshToken);
        
        RefreshReply refreshReply = new()
        {
            Tokens = _mapper.Map<TokenModel>(newTokens)
        };
        return Task.FromResult(refreshReply);
    }

    public override async Task<ValidateBasicReply> ValidateBasic(AuthRequest request, ServerCallContext context)
    {
        await _userService.AuthenticateAsync(request.Username, request.Password);
        return new ValidateBasicReply { Success = true };
    }
}