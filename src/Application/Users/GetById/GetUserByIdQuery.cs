using MediatR;

namespace Business.Users.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<UserResponse>;