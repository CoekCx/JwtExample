using FluentResults;
using FluentValidation;
using MediatR;

namespace Business.Behaviors;

/// <summary>
/// Represents the validation behavior middleware.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : ResultBase
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    /// <summary>
    /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest,TResponse}"/> class.
    /// </summary>
    /// <param name="validators">The validator for the current request type.</param>
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);

        var errors = _validators
            .Select(x => x.Validate(context))
            .SelectMany(x => x.Errors)
            .Where(x => x != null)
            .Select(x => new Error(x.ErrorMessage))
            .ToList();

        if (errors.Any())
        {
            // Create a new Result with errors
            var result = Result.Fail(errors);
            return (TResponse)(ResultBase)result;
        }

        return await next();
    }
}