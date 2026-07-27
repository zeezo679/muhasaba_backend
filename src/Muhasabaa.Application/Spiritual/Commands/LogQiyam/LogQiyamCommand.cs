using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Spiritual.Commands.LogQiyam;

public sealed record LogQiyamCommand(Guid UserId, bool PrayedQiyam) : IRequest<ErrorOr<Updated>>;
