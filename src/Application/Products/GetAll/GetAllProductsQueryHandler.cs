using Business.Abstractions.Data;
using Business.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.GetAll;

internal sealed class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductResponse>>
{
    private readonly IApplicationDbContext _dbContext;

    public GetAllProductsQueryHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<IEnumerable<ProductResponse>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken) =>
        await _dbContext.Products
            .Select(x=> new ProductResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price))
            .ToListAsync(cancellationToken);
}