using FluentResults;
using Microsoft.AspNetCore.Http;

namespace JwtExamples.MinimalApi.Extensions;

public static class ResultExtensions
{
    public static IResult ToMinimalApiResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }

        var error = result.Errors.First();
        return error.Metadata.GetValueOrDefault("ErrorType") switch
        {
            "NotFound" => Results.NotFound(error.Message),
            "Validation" => Results.BadRequest(error.Message),
            "Authentication" => Results.Unauthorized(),
            _ => Results.StatusCode(500)
        };
    }

    public static IResult ToMinimalApiResult(this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.NoContent();
        }

        var error = result.Errors.First();
        return error.Metadata.GetValueOrDefault("ErrorType") switch
        {
            "NotFound" => Results.NotFound(error.Message),
            "Validation" => Results.BadRequest(error.Message),
            "Authentication" => Results.Unauthorized(),
            _ => Results.StatusCode(500)
        };
    }
} 