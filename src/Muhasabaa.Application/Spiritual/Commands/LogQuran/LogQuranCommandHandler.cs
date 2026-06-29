using ErrorOr;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.DailyLogs;

namespace Muhasabaa.Application.Spiritual.Commands.LogQuran;

public sealed class LogQuranCommandHandler(
    IAppDbContext dbContext,
    IDailyLogService dailyLogService)
    : IRequestHandler<LogQuranCommand, ErrorOr<Updated>>
{
    public async Task<ErrorOr<Updated>> Handle(LogQuranCommand request, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var dailyLog = await dbContext.DailyLogs
            .SingleOrDefaultAsync(d => d.UserId == request.UserId && d.Date == today, cancellationToken);

        if (dailyLog is null)
        {
            var createResult = DailyLog.Create(request.UserId, today, quranPages: request.Pages);

            if (createResult.IsError)
                return createResult.Errors;

            dailyLog = createResult.Value;
            dbContext.DailyLogs.Add(dailyLog);
        }
        else
        {
            dailyLog.Update(quranPages: request.Pages);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        var recalcResult = await dailyLogService.RecalculateAsync(request.UserId, cancellationToken);
        if (recalcResult.IsError)
            return recalcResult.Errors;

        return Result.Updated;
    }
}
