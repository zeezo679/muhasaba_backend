// src/Muhasabaa.Application/Prayers/Commands/LogPrayer/LogPrayerCommandHandler.cs
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Domain.Errors;

namespace Muhasabaa.Application.Prayers.Commands.LogPrayer;

public sealed class LogPrayerCommandHandler(
    IAppDbContext dbContext,
    UserManager<ApplicationUser> userManager,
    IDailyLogService dailyLogService)
    : IRequestHandler<LogPrayerCommand, ErrorOr<LogPrayerResult>>
{
    public async Task<ErrorOr<LogPrayerResult>> Handle(LogPrayerCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Error.NotFound("User.NotFound", "User not found.");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        //race condition fixed by adding and index to userId , date , prayerName
        var alreadyLogged = await dbContext.PrayerLogs
            .AnyAsync(p => p.UserId == request.UserId
                           && p.Date == today
                           && p.PrayerName == request.PrayerName,
                cancellationToken);

        if (alreadyLogged)
            return PrayerLogErrors.AlreadyLogged;


        var result = PrayerLog.Create(
            request.UserId,
            request.PrayerName,
            request.Status,
            today,
            user.Gender,
            request.PrayedSunnah);

        if (result.IsError)
            return result.Errors;

        var log = result.Value;
        dbContext.PrayerLogs.Add(log);
        await dbContext.SaveChangesAsync(cancellationToken);

        var dailyLog = await dbContext.DailyLogs
            .SingleOrDefaultAsync(d => d.UserId == request.UserId && d.Date == today, cancellationToken);

        if (dailyLog is null)
        {
            var newLog = DailyLog.Create(request.UserId, today);

            if (newLog.IsError) return newLog.Errors;

            dbContext.DailyLogs.Add(newLog.Value);
            
            await dbContext.SaveChangesAsync(cancellationToken);
        }

        await dailyLogService.RecalculateAsync(request.UserId, cancellationToken);

        return new LogPrayerResult(log.Id, log.PrayerName, log.Status, log.PrayedSunnah, log.Score, log.MaximumScore, log.Date);
    }
}