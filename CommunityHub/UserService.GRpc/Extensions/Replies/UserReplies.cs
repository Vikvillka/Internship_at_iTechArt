using UserService.GRpc.Common;
using UserService.GRpc.Interfaces;

namespace UserService.GRpc;

public partial class RegisterUserReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class DeleteUserReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class GetUserReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class GetUsersReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}
