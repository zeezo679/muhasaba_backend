using ErrorOr;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.Habits;
using Muhasabaa.Domain.Entities.Helpers;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Infrastructure.Services;

public class DailyLogService(
    IAppDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    DailyScoreCalculator calculator) : IDailyLogService
{
    public async Task<ErrorOr<Updated>> RecalculateAsync(Guid userId, CancellationToken ct = default)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var dailyLog = await dbContext.DailyLogs
            .SingleOrDefaultAsync(d => d.UserId == userId && d.Date == today, ct);

        if (dailyLog is null)
            return Error.NotFound("DailyLog.NotFound", "No daily log found for today.");

        var prayers = await dbContext.PrayerLogs
            .Where(p => p.UserId == userId && p.Date == today)
            .ToListAsync(ct);

        var user = await userManager.FindByIdAsync(userId.ToString());

        var score = calculator.Calculate(
            prayers,
            dailyLog.DhikrCount,
            dailyLog.QuranPages,
            dailyLog.GymMinutes,
            dailyLog.ScreenTimeHours,
            dailyLog.PrayedQiyam,
            Enumerable.Empty<CustomHabitLog>(),
            user?.Gender);

        dailyLog.Recalculate(score);
        await dbContext.SaveChangesAsync(ct);

        return Result.Updated;
    }
}
