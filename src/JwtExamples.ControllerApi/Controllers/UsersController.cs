using Business.Shared;
using Business.Users.GetById;
using Business.Users.Login;
using Business.Users.Register;
using Infrastructure.Extensions;
using JwtExamples.ControllerApi.Abstractions;
using JwtExamples.ControllerApi.Extensions;
using JwtExamples.ControllerApi.Requests.Users;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtExamples.ControllerApi.Controllers;

[Authorize]
[Route("api/[controller]")]
public sealed class UsersController : BaseController
{
    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="request">The create user request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user identifier.</returns>
    [HttpPost("register")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<RegisterUserCommand>();
        var result = await Sender.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }

    /// <summary>
    /// Get user by specified identifier.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user details with the specified identifier.</returns>
    [HttpGet("{userId:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetById(Guid userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);

        var response = await Sender.Send(query, cancellationToken);

        return Ok(response);
    }

    /// <summary>
    /// Gets user details for logged user.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>User details.</returns>
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLoggedUser(CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(HttpContext.User.GetUserId());
        var result = await Sender.Send(query, cancellationToken);
        return result.ToActionResult(this);
    }
    
    /// <summary>
    /// Register new user.
    /// </summary>
    /// <param name="request">The create user request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The user identifier.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var command = request.Adapt<LoginUserCommand>();
        var result = await Sender.Send(command, cancellationToken);
        return result.ToActionResult(this);
    }
}