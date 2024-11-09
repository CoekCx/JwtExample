using FluentResults;

namespace Business.Common.Results.Errors;

public class ValidationError : Error
{
    public ValidationError(string message) : base(message)
    {
        Metadata.Add("ErrorType", "Validation");
    }
} 