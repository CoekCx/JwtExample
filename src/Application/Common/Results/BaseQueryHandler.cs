using FluentResults;
using MediatR;

namespace Business.Common.Results;

public abstract class BaseQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IRequest<Result<TResponse>>
{
    public abstract Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
} 