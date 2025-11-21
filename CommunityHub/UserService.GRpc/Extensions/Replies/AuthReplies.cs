using UserService.GRpc.Common;
using UserService.GRpc.Interfaces;

namespace UserService.GRpc;

public partial class GetTokensReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class RefreshReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class ValidateBasicReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}