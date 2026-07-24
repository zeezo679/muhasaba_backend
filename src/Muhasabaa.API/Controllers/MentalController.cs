using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muhasabaa.Application.Mental.Commands;
using System.Security.Claims;

namespace Muhasabaa.API.Controllers;

[ApiController]
[Route("api/mental")]
[Authorize]
public sealed class MentalController(ISender sender) : AppBaseController
{
    [HttpPost("deep-work")]
    public async Task<IActionResult> LogDeepWork(LogDeepWorkRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new LogDeepWorkCommand(userId, request.Hours);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => Ok(), Problem);
    }

    [HttpPost("screen-time")]
    public async Task<IActionResult> LogScreenTime(LogScreenTimeRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new LogScreenTimeCommand(userId, request.Hours);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => Ok(), Problem);
    }
}

public sealed record LogDeepWorkRequest(int Hours);
public sealed record LogScreenTimeRequest(int Hours);
