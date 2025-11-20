using UserService.GRpc.Common;
using UserService.GRpc.Interfaces;

namespace UserService.GRpc;

public partial class CreateSubscriptionReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class UnsubscribeReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

public partial class GetUserSubscriptionsReply : IProblemReply
{
    public void SetProblem(ProblemDetails problem) => Problem = problem;
}

