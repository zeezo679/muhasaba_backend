using System;

namespace Muhasabaa.Application.DailyLogs.Queries.GetTodayDailyLog;

public sealed record DailyLogResult(
    int DhikrCount,
    int QuranPages,
    int GymMinutes,
    bool PrayedQiyam,
    int SleepHours,
    int ScreenTimeHours,
    int DeepWorkHours,
    int EarnedScore,
    int MaximumScore,
    int Percentage,
    DateTime CalculatedAt);
