using Business.Abstractions.Data;
using Business.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.GetById;

internal sealed class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductResponse?>
{
    private readonly IApplicationDbContext _dbContext;

    public GetProductByIdQueryHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<ProductResponse?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        await _dbContext.Products
            .Where(x => x.Id == request.Id)
            .Select(x => new ProductResponse(
                x.Id,
                x.Name,
                x.Description,
                x.Price))
            .FirstOrDefaultAsync(cancellationToken);
}