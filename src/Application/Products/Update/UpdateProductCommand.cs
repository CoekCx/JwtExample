using FluentResults;
using MediatR;

namespace Business.Products.Update;

public sealed record UpdateProductCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price) : IRequest<Result>;