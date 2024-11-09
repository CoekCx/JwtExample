using FluentResults;
using Microsoft.AspNetCore.Http;

namespace JwtExamples.MinimalApi.Extensions;

public static class ResultExtensions
{
    public static IResult ToHttpResult<T>(this Result<T> result) =>
        result.Match<IResult>(
            success => Results.Ok(success),
            errors => errors.First().Metadata.GetValueOrDefault("ErrorType") switch
            {
                "NotFound" => Results.NotFound(errors),
                "Validation" => Results.BadRequest(errors),
                "Authentication" => Results.Unauthorized(),
                _ => Results.StatusCode(500)
            });

    public static IResult ToHttpResult(this Result result) =>
        result.Match<IResult>(
            () => Results.Ok(),
            errors => errors.First().Metadata.GetValueOrDefault("ErrorType") switch
            {
                "NotFound" => Results.NotFound(errors),
                "Validation" => Results.BadRequest(errors),
                "Authentication" => Results.Unauthorized(),
                _ => Results.StatusCode(500)
            });
} 