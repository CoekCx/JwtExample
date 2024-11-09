using Business.Products.GetAll;
using JwtExamples.MinimalApi.Abstractions;
using JwtExamples.MinimalApi.Constants;
using MediatR;

namespace JwtExamples.MinimalApi.Products;

public sealed class GetAllEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/", async (ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetAllProductsQuery();

            var response = await sender.Send(query, cancellationToken);

            return response;
        }).WithTags(Tags.Products);
    }
}