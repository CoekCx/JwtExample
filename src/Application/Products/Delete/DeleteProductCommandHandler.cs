using FluentResults;
using Business.Abstractions.Data;
using Business.Common.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Delete;

internal sealed class DeleteProductCommandHandler : BaseCommandHandler<DeleteProductCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteProductCommandHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.Id} not found"));
        }

        try
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to delete product").CausedBy(ex));
        }
    }
}