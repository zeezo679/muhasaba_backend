using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Physical.Commands;

public sealed record LogSleepCommand(Guid UserId, int Hours) : IRequest<ErrorOr<Updated>>;
