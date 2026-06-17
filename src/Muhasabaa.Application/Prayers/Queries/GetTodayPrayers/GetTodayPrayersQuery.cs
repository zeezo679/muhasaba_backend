using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Prayers.Queries.GetTodayPrayers;

public sealed record GetTodayPrayersQuery(Guid UserId) : IRequest<ErrorOr<List<PrayerLogResult>>>;

public sealed record PrayerLogResult(
    Guid? Id,
    string PrayerName,
    string? Status,
    bool? PrayedSunnah,
    int? Score,
    int? MaximumScore,
    bool IsLogged);