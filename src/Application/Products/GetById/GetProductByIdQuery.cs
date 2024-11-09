using Business.Shared;
using FluentResults;
using MediatR;

namespace Business.Products.GetById;

public sealed record GetProductByIdQuery(Guid Id) : IRequest<ProductResponse?>, IRequest<Result<ProductResponse>>;
