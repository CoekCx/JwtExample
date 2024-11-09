using FluentResults;
using Business.Abstractions.Data;
using Domain.Entities;
using MediatR;

namespace Business.Products.Create;

internal sealed class CreateProductCommandHandler : BaseCommandHandler<CreateProductCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;

    public CreateProductCommandHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Result<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product(
                Guid.NewGuid(),
                DateTime.UtcNow,
                request.Name,
                request.Description,
                request.Price);

            _dbContext.Products.Add(product);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return Result.Ok(product.Id);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to create product").CausedBy(ex));
        }
    }
}