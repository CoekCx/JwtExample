using MediatR;

namespace Business.Products.Create;

public sealed record CreateProductCommand(
    string Name,
    string Description,
    decimal Price) : IRequest<Guid>;