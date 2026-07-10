using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.DailyLogs;

namespace Muhasabaa.Application.Physical.Commands;

public class LogSleepCommandHandler(IAppDbContext dbContext)
    : IRequestHandler<LogSleepCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(LogSleepCommand request, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var dailyLogResult = await FetchOrCreateDailyLog(request.UserId, today, request.Hours, ct);
        if (dailyLogResult.IsError)
            return dailyLogResult.Errors;

        await dbContext.SaveChangesAsync(ct);

        return Result.Updated;
    }

    private async Task<ErrorOr<DailyLog>> FetchOrCreateDailyLog(
        Guid userId, DateOnly today, int sleepHours, CancellationToken ct)
    {
        var dailyLog = await dbContext.DailyLogs
            .SingleOrDefaultAsync(d => d.UserId == userId && d.Date == today, ct);

        if (dailyLog is null)
        {
            var createResult = DailyLog.Create(userId, today, sleepHours: sleepHours);
            if (createResult.IsError)
                return createResult.Errors;

            dailyLog = createResult.Value;
            dbContext.DailyLogs.Add(dailyLog);
        }
        else
        {
            dailyLog.Update(sleepHours: sleepHours);
        }

        return dailyLog;
    }
}
