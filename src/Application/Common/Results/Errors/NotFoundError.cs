using FluentResults;

namespace Business.Common.Results.Errors;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    {
        Metadata.Add("ErrorType", "NotFound");
    }
} 