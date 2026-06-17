// src/Muhasabaa.Application/Prayers/Commands/LogPrayer/LogPrayerCommandHandler.cs
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Domain.Errors;

namespace Muhasabaa.Application.Prayers.Commands.LogPrayer;

public sealed class LogPrayerCommandHandler(
    IAppDbContext dbContext,
    UserManager<ApplicationUser> userManager)
    : IRequestHandler<LogPrayerCommand, ErrorOr<LogPrayerResult>>
{
    public async Task<ErrorOr<LogPrayerResult>> Handle(LogPrayerCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        if (user is null)
            return Error.NotFound("User.NotFound", "User not found.");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

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

        return new LogPrayerResult(log.Id, log.PrayerName, log.Status, log.PrayedSunnah, log.Score, log.MaximumScore, log.Date);
    }
}