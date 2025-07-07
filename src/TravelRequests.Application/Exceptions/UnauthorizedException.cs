namespace TravelRequests.Application.Exceptions;

public class UnauthorizedException : Exception
{
    public UnauthorizedException()
        : base("No está autorizado para realizar esta acción.")
    {
    }

    public UnauthorizedException(string message)
        : base(message)
    {
    }

    public UnauthorizedException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
} 