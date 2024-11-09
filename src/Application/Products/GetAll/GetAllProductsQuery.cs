using Business.Shared;
using MediatR;

namespace Business.Products.GetAll;

public sealed record GetAllProductsQuery : IRequest<IEnumerable<ProductResponse>>;
