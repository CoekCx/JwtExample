﻿using Business.Users.Register;
using JwtExamples.MinimalApi.Abstractions;
using JwtExamples.MinimalApi.Constants;
using JwtExamples.MinimalApi.Extensions;
using Mapster;
using MediatR;

namespace JwtExamples.MinimalApi.Users;

internal sealed record RegisterRequest(string Email, string Password, string FirstName, string LastName);

internal sealed class RegisterEndpoint : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (ISender sender, RegisterRequest request, CancellationToken cancellationToken) =>
        {
            var command = request.Adapt<RegisterUserCommand>();
            var result = await sender.Send(command, cancellationToken);
            return result.ToMinimalApiResult();
        }).WithTags(Tags.Users)
          .AllowAnonymous();
    }
}