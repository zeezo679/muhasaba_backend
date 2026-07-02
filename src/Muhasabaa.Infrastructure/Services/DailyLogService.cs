using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Habits;
using Muhasabaa.Domain.Entities.Helpers;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Infrastructure.Services;

public class DailyLogService(DailyScoreCalculator calculator) : IDailyLogService
{
    public ErrorOr<Updated> Recalculate(DailyLog latestDailyLog, IReadOnlyCollection<PrayerLog> todaysPrayerLogs, Gender? userGender)
    {
        var score = calculator.Calculate(
            todaysPrayerLogs,
            latestDailyLog.DhikrCount,
            latestDailyLog.QuranPages,
            latestDailyLog.GymMinutes,
            latestDailyLog.ScreenTimeHours,
            latestDailyLog.PrayedQiyam,
            Enumerable.Empty<CustomHabitLog>(),
            userGender);

        latestDailyLog.Recalculate(score);
        return Result.Updated;
        
    }
    
}
