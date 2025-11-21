using UserService.GRpc.Common;
using UserService.GRpc.Interfaces;

namespace UserService.GRpc;

public partial class CreateParticipationReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class CancelParticipationReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class GetUserParticipationsReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}