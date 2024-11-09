using Business.Abstractions.Authentication;
using Business.Abstractions.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Users.Login;

internal sealed class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, string>
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

    public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email == request.Email, cancellationToken);

        if (user is null)
        {
            throw new Exception("User not found by email.");
        }

        if (!_passwordHasher.Verify(request.Password, user.Password))
        {
            throw new Exception("Incorrect password");
        }

        return _jwtProvider.Generate(user.Id, user.Email);
    }
}