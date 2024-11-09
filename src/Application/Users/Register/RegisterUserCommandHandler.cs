using FluentResults;
using Business.Abstractions.Authentication;
using Business.Abstractions.Data;
using Business.Common.Results;
using Business.Common.Errors;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Business.Users.Register;

internal sealed class RegisterUserCommandHandler : BaseCommandHandler<RegisterUserCommand, Guid>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IApplicationDbContext dbContext, IPasswordHasher passwordHasher)
    {
        _dbContext = dbContext;
        _passwordHasher = passwordHasher;
    }

    public override async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var exists = await _dbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken);
        if (exists)
        {
            return Result.Fail(new ValidationError("Email already taken."));
        }

        try 
        {
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

            return Result.Ok(user.Id);
        }
        catch (Exception ex)
        {
            return Result.Fail(new Error("Failed to create user").CausedBy(ex));
        }
    }
}