using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Physical.Commands;

public sealed record LogGymCommand(Guid UserId, int Minutes) : IRequest<ErrorOr<Updated>>;