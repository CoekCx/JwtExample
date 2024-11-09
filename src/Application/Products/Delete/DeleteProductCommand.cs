using FluentResults;
using MediatR;

namespace Business.Products.Delete;

public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;
