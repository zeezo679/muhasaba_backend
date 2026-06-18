using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Application.Prayers.Queries.GetTodayPrayers;

public sealed class GetTodayPrayersQueryHandler(IAppDbContext dbContext)
    : IRequestHandler<GetTodayPrayersQuery, ErrorOr<List<PrayerLogResult>>>
{
    private static readonly PrayerName[] AllPrayers =
        [PrayerName.Fajr, PrayerName.Dhuhr, PrayerName.Asr, PrayerName.Maghrib, PrayerName.Isha];

    public async Task<ErrorOr<List<PrayerLogResult>>> Handle(GetTodayPrayersQuery request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        //converted from a list to a dictionary for better performance , O(n*m) to O(1) lookup time
        var logs = await dbContext.PrayerLogs
            .AsNoTracking()
            .Where(p => p.UserId == request.UserId && p.Date == today)
            .ToDictionaryAsync(p => p.PrayerName, p => p, cancellationToken);
        

        var result = AllPrayers.Select(prayer =>
        {
            logs.TryGetValue(prayer, out var log);

            return log is not null
                ? new PrayerLogResult(log.Id, prayer.ToString(), log.Status.ToString(), log.PrayedSunnah, log.Score, log.MaximumScore, true)
                : new PrayerLogResult(null, prayer.ToString(), null, null, null, null, false);

        }).ToList();

        return result;
    }
}