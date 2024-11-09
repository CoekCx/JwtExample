using FluentResults;
using Business.Abstractions.Authentication;
using Business.Abstractions.Data;
using Business.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Users.Login;

internal sealed class LoginUserCommandHandler : BaseCommandHandler<LoginUserCommand, string>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public LoginUserCommandHandler(
        IApplicationDbContext dbContext,
        IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public override async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is null)
        {
            return Result.Fail(new Error("User not found by email."));
        }

        if (!_passwordHasher.Verify(request.Password, user.Password))
        {
            return Result.Fail(new Error("Incorrect password"));
        }

        var token = _jwtProvider.Generate(user.Id, user.Email);
        return Result.Ok(token);
    }
}