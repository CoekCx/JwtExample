using Business.Shared;
using MediatR;

namespace Business.Products.GetById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>;
