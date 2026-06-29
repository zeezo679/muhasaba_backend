using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muhasabaa.Application.DailyLogs.Queries.GetTodayDailyLog;
using System.Security.Claims;

namespace Muhasabaa.API.Controllers;

[ApiController]
[Route("api/daily-log")]
[Authorize]
public sealed class DailyLogController(ISender sender) : AppBaseController
{
    [HttpGet("today")]
    public async Task<IActionResult> GetTodayDailyLog(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var query = new GetTodayDailyLogQuery(userId);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(r => Ok(r), Problem);
    }
}
