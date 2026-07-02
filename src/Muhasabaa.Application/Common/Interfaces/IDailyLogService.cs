using ErrorOr;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Application.Common.Interfaces;

public interface IDailyLogService
{
    ErrorOr<Updated> Recalculate(DailyLog dailyLog, IReadOnlyCollection<PrayerLog> todaysPrayerLogs, Gender? gender);
}
