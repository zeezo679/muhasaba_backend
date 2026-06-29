using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Spiritual.Commands.LogQuran;

public sealed record LogQuranCommand(Guid UserId, int Pages) : IRequest<ErrorOr<Updated>>;
