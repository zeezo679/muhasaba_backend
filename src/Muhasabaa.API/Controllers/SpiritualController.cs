using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muhasabaa.Application.Spiritual.Commands.LogDhikr;
using Muhasabaa.Application.Spiritual.Commands.LogQuran;
using System.Security.Claims;

namespace Muhasabaa.API.Controllers;


[ApiController]
[Route("api/spiritual")]
[Authorize]
public sealed class SpiritualController(ISender sender) : AppBaseController
{
    [HttpPost("dhikr")]
    public async Task<IActionResult> LogDhikr(LogDhikrRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new LogDhikrCommand(userId, request.DhikrCount);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => Ok(), Problem);
    }

    [HttpPost("quran")]
    public async Task<IActionResult> LogQuran(LogQuranRequest request, CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var command = new LogQuranCommand(userId, request.Pages);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => Ok(), Problem);
    }
}

public sealed record LogDhikrRequest(int DhikrCount);
public sealed record LogQuranRequest(int Pages);
