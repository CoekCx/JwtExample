using FluentResults;
using Business.Shared;
using MediatR;

namespace Business.Products.GetAll;

public sealed record GetAllProductsQuery() : IRequest<Result<IEnumerable<ProductResponse>>>;
