using ErrorOr;
using MediatR;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Application.Prayers.Commands.LogPrayer;

public sealed record LogPrayerCommand(
    Guid UserId,
    PrayerName PrayerName,
    PrayerStatus Status,
    bool PrayedSunnah) : IRequest<ErrorOr<LogPrayerResult>>;

public sealed record LogPrayerResult(Guid Id, PrayerName PrayerName, PrayerStatus Status, bool PrayedSunnah, int Score, int MaximumScore, DateOnly Date);