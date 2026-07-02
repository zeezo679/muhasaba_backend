using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Application.Spiritual.Commands.LogQuran;

public sealed class LogQuranCommandHandler(
    IAppDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    IDailyLogService dailyLogService)
    : IRequestHandler<LogQuranCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(LogQuranCommand request, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var dailyLogResult = await FetchOrCreateDailyLog(request.UserId, today, request.Pages, ct);
        if (dailyLogResult.IsError)
            return dailyLogResult.Errors;

        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Error.NotFound("User.NotFound", "User not found.");

        var todaysPrayerLogs = await FetchTodaysPrayerLogs(request.UserId, today, ct);

        var recalcResult = dailyLogService.Recalculate(dailyLogResult.Value, todaysPrayerLogs, user.Gender);
        if (recalcResult.IsError)
            return recalcResult.Errors;

        await dbContext.SaveChangesAsync(ct);

        return Result.Updated;
    }

    private async Task<ErrorOr<DailyLog>> FetchOrCreateDailyLog(
        Guid userId, DateOnly today, int quranPages, CancellationToken ct)
    {
        var dailyLog = await dbContext.DailyLogs
            .SingleOrDefaultAsync(d => d.UserId == userId && d.Date == today, ct);

        if (dailyLog is null)
        {
            var createResult = DailyLog.Create(userId, today, quranPages: quranPages);
            if (createResult.IsError)
                return createResult.Errors;

            dailyLog = createResult.Value;
            dbContext.DailyLogs.Add(dailyLog);
        }
        else
        {
            dailyLog.Update(quranPages: quranPages);
        }

        return dailyLog;
    }

    private async Task<List<PrayerLog>> FetchTodaysPrayerLogs(Guid userId, DateOnly today, CancellationToken ct)
    {
        return await dbContext.PrayerLogs
            .Where(p => p.UserId == userId && p.Date == today)
            .ToListAsync(ct);
    }
}