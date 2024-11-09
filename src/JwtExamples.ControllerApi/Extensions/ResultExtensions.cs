using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace JwtExamples.ControllerApi.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller) =>
        result.Match<IActionResult>(
            success => controller.Ok(success),
            errors => errors.First().Metadata.GetValueOrDefault("ErrorType") switch
            {
                "NotFound" => controller.NotFound(errors),
                "Validation" => controller.BadRequest(errors),
                "Authentication" => controller.Unauthorized(),
                _ => controller.StatusCode(500)
            });

    public static IActionResult ToActionResult(this Result result, ControllerBase controller) =>
        result.Match<IActionResult>(
            () => controller.NoContent(),
            errors => errors.First().Metadata.GetValueOrDefault("ErrorType") switch
            {
                "NotFound" => controller.NotFound(errors),
                "Validation" => controller.BadRequest(errors),
                "Authentication" => controller.Unauthorized(),
                _ => controller.StatusCode(500)
            });
}
