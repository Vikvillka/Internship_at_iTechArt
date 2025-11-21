using UserService.GRpc.Common;

namespace Gateway.API.Extensions.Mappings.Convertors;

public class GrpcProblemDetailsException : Exception
{
    public string Title { get; }
    public int Status { get; }
    public string Instance { get; }

    public GrpcProblemDetailsException(ProblemDetails problem)
        : base(problem.Detail)
    {
        Title = problem.Title;
        Status = problem.Status;
        Instance = problem.Instance;
    }
}
