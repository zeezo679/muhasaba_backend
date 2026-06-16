// src/Muhasabaa.API/Controllers/AuthController.cs
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Muhasabaa.Application.Auth.Login;
using Muhasabaa.Application.Auth.Logout;
using Muhasabaa.Application.Auth.Refresh;
using Muhasabaa.Application.Auth.Register;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(ISender sender) : AppBaseController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = new RegisterCommand(request.Name, request.Email, request.Password, request.Gender);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(authResult => Ok(authResult), Problem);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var command = new LoginCommand(request.Email, request.Password);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(authResult => Ok(authResult), Problem);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshRequest request, CancellationToken cancellationToken)
    {
        var command = new RefreshCommand(request.RefreshToken);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(authResult => Ok(authResult), Problem);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(LogoutRequest request, CancellationToken cancellationToken)
    {
        var command = new LogoutCommand(request.RefreshToken);
        var result = await sender.Send(command, cancellationToken);
        return result.Match(_ => NoContent(), Problem);
    }
}

public sealed record RegisterRequest(string Name, string Email, string Password, Gender? Gender);
public sealed record LoginRequest(string Email, string Password);
public sealed record RefreshRequest(string RefreshToken);
public sealed record LogoutRequest(string RefreshToken);