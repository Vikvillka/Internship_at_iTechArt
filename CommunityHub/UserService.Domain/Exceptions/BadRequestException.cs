namespace UserService.Domain.Exceptions;

public class BadRequestException : Exception
{
    public string Error { get; }

    public BadRequestException(string error, string message) : base(message)
    {
        Error = error;
    }
}
