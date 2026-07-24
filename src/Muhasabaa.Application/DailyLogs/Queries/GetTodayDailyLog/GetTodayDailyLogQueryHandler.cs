using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;

namespace Muhasabaa.Application.DailyLogs.Queries.GetTodayDailyLog;

public sealed class GetTodayDailyLogQueryHandler(IAppDbContext dbContext)
    : IRequestHandler<GetTodayDailyLogQuery, ErrorOr<DailyLogResult>>
{
    public async Task<ErrorOr<DailyLogResult>> Handle(GetTodayDailyLogQuery request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var log = await dbContext.DailyLogs
            .AsNoTracking()
            .SingleOrDefaultAsync(d => d.UserId == request.UserId && d.Date == today, cancellationToken);

        if (log is null)
        {
            return new DailyLogResult(
                DhikrCount: 0,
                QuranPages: 0,
                GymMinutes: 0,
                PrayedQiyam: false,
                SleepHours: 0,
                ScreenTimeHours: 0,
                DeepWorkHours: 0,
                EarnedScore: 0,
                MaximumScore: 0,
                Percentage: 0,
                CalculatedAt: DateTime.UtcNow);
        }

        return new DailyLogResult(
            DhikrCount: log.DhikrCount,
            QuranPages: log.QuranPages,
            GymMinutes: log.GymMinutes,
            PrayedQiyam: log.PrayedQiyam,
            SleepHours: log.SleepHours,
            ScreenTimeHours: log.ScreenTimeHours,
            DeepWorkHours: log.DeepWorkHours,
            EarnedScore: log.EarnedScore,
            MaximumScore: log.MaximumScore,
            Percentage: log.Percentage,
            CalculatedAt: log.CalculatedAt);
    }
}
