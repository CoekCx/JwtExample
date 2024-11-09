using FluentResults;
using Business.Abstractions.Data;
using Business.Common.Results;
using Business.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.GetAll;

internal sealed class GetAllProductsQueryHandler : BaseQueryHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllProductsQueryHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public override async Task<Result<IEnumerable<ProductResponse>>> Handle(
        GetAllProductsQuery request, 
        CancellationToken cancellationToken)
    {
        try
        {
            var products = await _dbContext.Products
                .Select(x => new ProductResponse(
                    x.Id,
                    x.Name,
                    x.Description,
                    x.Price))
                .ToListAsync(cancellationToken);

            return Result.Ok(products.AsEnumerable());
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to retrieve products").CausedBy(ex));
        }
    }
}