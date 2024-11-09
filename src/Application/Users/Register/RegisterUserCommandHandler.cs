using Business.Abstractions.Authentication;
using Business.Abstractions.Data;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Business.Users.Register;

internal sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (exists)
        {
            throw new Exception("Email already taken.");
        }

        var hashedPassword = _passwordHasher.Hash(request.Password);

        var user = new User(
            Guid.NewGuid(),
            request.Email,
            hashedPassword,
            request.FirstName,
            request.LastName,
            DateTime.UtcNow);

        _dbContext.Users.Add(user);
        
        await _dbContext.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}