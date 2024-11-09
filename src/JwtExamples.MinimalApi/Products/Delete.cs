using Business.Products.Delete;
using JwtExamples.MinimalApi.Abstractions;
using JwtExamples.MinimalApi.Constants;
using JwtExamples.MinimalApi.Extensions;
using MediatR;

namespace JwtExamples.MinimalApi.Products;

public sealed class DeleteEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("products/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var command = new DeleteProductCommand(id);
            var result = await sender.Send(command, cancellationToken);
            return result.ToMinimalApiResult();
        }).WithTags(Tags.Products);
    }
}