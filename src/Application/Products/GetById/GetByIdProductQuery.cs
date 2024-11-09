using FluentResults;
using Business.Shared;
using MediatR;

namespace Business.Products.GetById;

public sealed record GetByIdProductQuery(Guid Id) : IRequest<Result<ProductResponse>>; 