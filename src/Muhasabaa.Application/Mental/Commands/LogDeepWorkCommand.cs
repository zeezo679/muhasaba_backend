using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Mental.Commands;

public sealed record LogDeepWorkCommand(Guid UserId, int Hours) : IRequest<ErrorOr<Updated>>;
