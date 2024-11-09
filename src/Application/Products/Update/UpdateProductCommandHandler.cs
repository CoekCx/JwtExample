using FluentResults;
using Business.Abstractions.Data;
using Business.Common.Errors;
using Business.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Products.Update;

internal sealed class UpdateProductCommandHandler : BaseCommandHandler<UpdateProductCommand>
{
    private readonly IApplicationDbContext _dbContext;

    public UpdateProductCommandHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public override async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _dbContext.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (product is null)
        {
            return Result.Fail(new NotFoundError($"Product with ID {request.Id} not found"));
        }

        try
        {
            product.Update(request.Name, request.Description, request.Price);
            await _dbContext.SaveChangesAsync(cancellationToken);
            
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to update product").CausedBy(ex));
        }
    }
}