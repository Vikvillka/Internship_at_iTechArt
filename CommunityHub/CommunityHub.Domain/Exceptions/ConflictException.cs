namespace CommunityHub.Domain.Exceptions;

public class ConflictException : Exception
{
    public string Error { get; }

    public ConflictException(string error, string message) : base(message)
    {
        Error = error;
    }
}
