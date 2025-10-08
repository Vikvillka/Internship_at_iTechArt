namespace CommunityHub.Domain.Exceptions;

public class NotFoundException : Exception
{
    public string Error { get; }

    public NotFoundException(string error, string message) : base(message)
    {
        Error = error;
    }
}

