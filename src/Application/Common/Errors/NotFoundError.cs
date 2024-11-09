using FluentResults;

namespace Business.Common.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    {
        Metadata.Add("ErrorType", "NotFound");
    }
} 