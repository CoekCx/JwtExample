using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace JwtExamples.ControllerApi.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            return controller.Ok(result.Value);
        }

        var error = result.Errors.First();
        return error.Metadata.GetValueOrDefault("ErrorType") switch
        {
            "NotFound" => controller.NotFound(error.Message),
            "Validation" => controller.BadRequest(error.Message),
            "Authentication" => controller.Unauthorized(),
            _ => controller.StatusCode(500)
        };
    }

    public static IActionResult ToActionResult(this Result result, ControllerBase controller)
    {
        if (result.IsSuccess)
        {
            return controller.NoContent();
        }

        var error = result.Errors.First();
        return error.Metadata.GetValueOrDefault("ErrorType") switch
        {
            "NotFound" => controller.NotFound(error.Message),
            "Validation" => controller.BadRequest(error.Message),
            "Authentication" => controller.Unauthorized(),
            _ => controller.StatusCode(500)
        };
    }
}
