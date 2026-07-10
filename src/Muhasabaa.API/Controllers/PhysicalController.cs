using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muhasabaa.Application.Physical.Commands;
using System.Security.Claims;

namespace Muhasabaa.API.Controllers;

[ApiController]
[Route("api/physical")]
[Authorize]
public sealed class PhysicalController(ISender sender) : AppBaseController
{
    [HttpPost("gym")]
    public async Task<IActionResult> LogGym(LogGymRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new LogGymCommand(userId, request.Minutes);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => Ok(), Problem);
    }

    [HttpPost("sleep")]
    public async Task<IActionResult> LogSleep(LogSleepRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new LogSleepCommand(userId, request.Hours);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => Ok(), Problem);
    }
}

public sealed record LogGymRequest(int Minutes);
public sealed record LogSleepRequest(int Hours);
