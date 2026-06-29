using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.DailyLogs.Queries.GetTodayDailyLog;

public sealed record GetTodayDailyLogQuery(Guid UserId) : IRequest<ErrorOr<DailyLogResult>>;
