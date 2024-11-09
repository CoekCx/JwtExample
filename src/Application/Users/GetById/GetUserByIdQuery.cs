using FluentResults;
using MediatR;

namespace Business.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserResponse>>;