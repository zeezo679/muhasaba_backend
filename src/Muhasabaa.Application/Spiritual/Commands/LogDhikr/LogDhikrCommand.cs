using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Spiritual.Commands.LogDhikr;

public sealed record LogDhikrCommand(Guid UserId, int DhikrCount) : IRequest<ErrorOr<Updated>>;
