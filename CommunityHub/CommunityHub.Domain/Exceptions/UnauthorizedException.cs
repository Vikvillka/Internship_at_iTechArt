namespace CommunityHub.Domain.Exceptions;

public class UnauthorizedException : Exception
{
    public string Error { get; }

    public UnauthorizedException(string error, string message) : base(message)
    {
        Error = error;
    }
}