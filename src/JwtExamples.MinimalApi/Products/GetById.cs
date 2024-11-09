using Business.Products.GetById;
using JwtExamples.MinimalApi.Abstractions;
using JwtExamples.MinimalApi.Constants;
using JwtExamples.MinimalApi.Extensions;
using MediatR;

namespace JwtExamples.MinimalApi.Products;

public sealed class GetByIdEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{id:guid}", async (ISender sender, Guid id, CancellationToken cancellationToken) =>
        {
            var query = new GetByIdProductQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.ToMinimalApiResult();
        }).WithTags(Tags.Products);
    }
}