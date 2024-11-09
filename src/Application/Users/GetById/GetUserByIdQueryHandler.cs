using FluentResults;
using Business.Abstractions.Data;
using Business.Common.Results;
using Business.Common.Errors;
using Microsoft.EntityFrameworkCore;

namespace Business.Users.GetById;

internal sealed class GetUserByIdQueryHandler : BaseQueryHandler<GetUserByIdQuery, UserResponse>
{
    private readonly IApplicationDbContext _dbContext;

    public GetUserByIdQueryHandler(IApplicationDbContext dbContext) =>
        _dbContext = dbContext;

    public override async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var response = await _dbContext.Users
            .Where(x => x.Id == request.Id)
            .Select(x => new UserResponse(
                x.Id,
                x.Email,
                x.FirstName,
                x.LastName,
                x.CreatedAt))
            .FirstOrDefaultAsync(cancellationToken);

        if (response is null)
        {
            return Result.Fail(new NotFoundError($"User with ID {request.Id} not found"));
        }

        return Result.Ok(response);
    }
}