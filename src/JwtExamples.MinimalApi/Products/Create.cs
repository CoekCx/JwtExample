using Business.Products.Create;
using JwtExamples.MinimalApi.Abstractions;
using JwtExamples.MinimalApi.Constants;
using JwtExamples.MinimalApi.Extensions;
using Mapster;
using MediatR;

namespace JwtExamples.MinimalApi.Products;

public sealed class CreateEndpoint : IEndpoint
{
    public sealed record CreateRequest(string Name, string Description, decimal Price);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (ISender sender, CreateRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<CreateProductCommand>();
            var result = await sender.Send(command, cancellationToken);
            return result.ToMinimalApiResult();
        }).WithTags(Tags.Products);
    }
}