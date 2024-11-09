using MediatR;

namespace Business.Users.Login;

public sealed record LoginUserCommand(string Email, string Password) : IRequest<string>;
