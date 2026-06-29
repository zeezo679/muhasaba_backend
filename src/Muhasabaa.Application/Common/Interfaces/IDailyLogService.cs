using ErrorOr;

namespace Muhasabaa.Application.Common.Interfaces;

public interface IDailyLogService
{
    Task<ErrorOr<Updated>> RecalculateAsync(Guid userId, CancellationToken ct = default);
}
