using FluentResults;

namespace Business.Common.Errors;

public class AuthenticationError : Error
{
    public AuthenticationError(string message) : base(message)
    {
        Metadata.Add("ErrorType", "Authentication");
    }
} 