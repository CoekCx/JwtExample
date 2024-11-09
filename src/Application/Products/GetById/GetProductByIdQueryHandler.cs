using FluentResults;
using Business.Abstractions.Data;
using Business.Common.Results;
using Business.Common.Errors;
using Business.Shared;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.GetById;

internal sealed class GetProductByIdQueryHandler : BaseQueryHandler<GetProductByIdQuery, ProductResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetProductByIdQueryHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public override async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.Products
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price))
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.Id} not found"));
        }

        return Result.Ok(response);
    }
}