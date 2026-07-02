// src/Muhasabaa.Application/Prayers/Commands/LogPrayer/LogPrayerCommandHandler.cs
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Domain.Enums;
using Muhasabaa.Domain.Errors;

namespace Muhasabaa.Application.Prayers.Commands.LogPrayer;

public sealed class LogPrayerCommandHandler(
    IAppDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    IDailyLogService dailyLogService)
    : IRequestHandler<LogPrayerCommand, ErrorOr<LogPrayerResult>>
{
    public async Task<ErrorOr<LogPrayerResult>> Handle(LogPrayerCommand request, CancellationToken ct)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Error.NotFound("User.NotFound", "User not found.");

        //fetching today's time to check if the user logged this prayer today or no
        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        

        var alreadyLogged = await dbContext.PrayerLogs
            .AnyAsync(p => p.UserId == request.UserId && p.Date == today && p.PrayerName == request.PrayerName, ct);
        if (alreadyLogged)
            return PrayerLogErrors.AlreadyLogged;
        
        //create the prayer log and add it to the db context, then recalculate the daily log
        var result = PrayerLog.Create(request.UserId, request.PrayerName, request.Status, today, user.Gender, request.PrayedSunnah);
        if (result.IsError)
            return result.Errors;

        var log = result.Value;
        dbContext.PrayerLogs.Add(log);

        var dailyLogResult = await FetchOrCreateDailyLog(request.UserId, today, ct);
        if (dailyLogResult.IsError)
            return dailyLogResult.Errors;

        var todaysPrayerLogs = await dbContext.PrayerLogs
            .Where(p => p.UserId == request.UserId && p.Date == today)
            .ToListAsync(ct);
        todaysPrayerLogs.Add(log); // not yet saved, won't come back from the query above

        var recalcResult = dailyLogService.Recalculate(dailyLogResult.Value, todaysPrayerLogs, user.Gender);
        if (recalcResult.IsError)
            return recalcResult.Errors;

        await dbContext.SaveChangesAsync(ct); // single save for everything

        return new LogPrayerResult(log.Id, log.PrayerName, log.Status, log.PrayedSunnah, log.Score, log.MaximumScore, log.Date);
    }

    private async Task<ErrorOr<DailyLog>> FetchOrCreateDailyLog(Guid userId, DateOnly today, CancellationToken ct)
    {
        var dailyLog = await dbContext.DailyLogs
            .SingleOrDefaultAsync(d => d.UserId == userId && d.Date == today, ct);

        if (dailyLog is null)
        {
            var newLog = DailyLog.Create(userId, today);
            if (newLog.IsError) return newLog.Errors;

            dbContext.DailyLogs.Add(newLog.Value);
            dailyLog = newLog.Value;
        }

        return dailyLog;
    }
}