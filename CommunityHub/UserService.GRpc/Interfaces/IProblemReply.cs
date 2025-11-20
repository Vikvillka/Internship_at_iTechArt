using UserService.GRpc.Common;

namespace UserService.GRpc.Interfaces;

public interface IProblemReply
{
    void SetProblem(ProblemDetails problem);
}
