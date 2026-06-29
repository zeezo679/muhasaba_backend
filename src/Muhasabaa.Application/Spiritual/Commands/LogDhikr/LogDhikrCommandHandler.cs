using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.DailyLogs;

namespace Muhasabaa.Application.Spiritual.Commands.LogDhikr;

public sealed class LogDhikrCommandHandler(
    IAppDbContext dbContext,
    IDailyLogService dailyLogService)
    : IRequestHandler<LogDhikrCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(LogDhikrCommand request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var dailyLog = await dbContext.DailyLogs
            .SingleOrDefaultAsync(d => d.UserId == request.UserId && d.Date == today, cancellationToken); //fetch daily log

        if (dailyLog is null) //check if daily log is not yet created?
        {
            //create it
            var createResult = DailyLog.Create(request.UserId, today, dhikrCount: request.DhikrCount);

            if (createResult.IsError)
                return createResult.Errors;

            //save to db
            dailyLog = createResult.Value;
            dbContext.DailyLogs.Add(dailyLog);
        }
        else
        { 
            //update if exist to make score calculation.
            dailyLog.Update(dhikrCount: request.DhikrCount);
        }

        //persist changes
        await dbContext.SaveChangesAsync(cancellationToken);

        //recalculate score
        var recalcResult = await dailyLogService.RecalculateAsync(request.UserId, cancellationToken);
        if (recalcResult.IsError)
            return recalcResult.Errors;

        return Result.Updated;
    }
}
