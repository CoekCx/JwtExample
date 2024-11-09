using FluentResults;
using MediatR;

namespace Business.Common.Results;

public abstract class BaseCommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : IRequest<Result>
{
    public abstract Task<Result> Handle(TCommand command, CancellationToken cancellationToken);
}

public abstract class BaseCommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : IRequest<Result<TResponse>>
{
    public abstract Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken);
} 