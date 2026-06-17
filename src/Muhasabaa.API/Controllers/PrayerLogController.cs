using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muhasabaa.Application.Prayers.Commands.LogPrayer;
using Muhasabaa.Application.Prayers.Queries.GetTodayPrayers;
using Muhasabaa.Domain.Enums;
using System.Security.Claims;

namespace Muhasabaa.API.Controllers;

[ApiController]
[Route("api/prayers")]
[Authorize]
public sealed class PrayerController(ISender sender) : AppBaseController
{
    [HttpPost]
    public async Task<IActionResult> LogPrayer(LogPrayerRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new LogPrayerCommand(userId, request.PrayerName, request.Status, request.PrayedSunnah);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(r => Ok(r), Problem);
    }

    [HttpGet("today")]
    public async Task<IActionResult> GetTodayPrayers(CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var query = new GetTodayPrayersQuery(userId);
        var result = await sender.Send(query, cancellationToken);
        return result.Match(r => Ok(r), Problem);
    }
}

public sealed record LogPrayerRequest(PrayerName PrayerName, PrayerStatus Status, bool PrayedSunnah);