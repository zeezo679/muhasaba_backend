// src/Muhasabaa.API/Controllers/AuthController.cs
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Muhasabaa.Application.Auth.Login;
using Muhasabaa.Application.Auth.Logout;
using Muhasabaa.Application.Auth.Refresh;
using Muhasabaa.Application.Auth.Register;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.API.Controllers;

[ApiController]
[Route("api/auth")]
[EnableRateLimiting("auth")]
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

    //smoke testing the resend api works on monsterASP
    [HttpGet("test-resend")]
    public async Task<IActionResult> TestResend(IHttpClientFactory clientFactory)
    {
        var client = clientFactory.CreateClient();
    
        // Set a short timeout so your request doesn't hang indefinitely if blocked
        client.Timeout = TimeSpan.FromSeconds(5);

        try
        {
            // Resend's base API URL (GET returns a 404 or 401, which proves connectivity works)
            var response = await client.GetAsync("https://api.resend.com/emails");
            
            return Ok(new 
            { 
                Success = true, 
                StatusCode = (int)response.StatusCode, 
                Message = "Egress successful! Host reached api.resend.com." 
            });
        }
        catch (TaskCanceledException)
        {
            return Problem("Network Timeout: Outbound port 443 might be blocked or proxied by MonsterASP.");
        }
        catch (HttpRequestException ex)
        {
            return Problem($"HTTP/TLS Error: {ex.InnerException?.Message ?? ex.Message}");
        }
        catch (Exception ex)
        {
            return Problem($"Unexpected Error: {ex.Message}");
        }
    }
}

public sealed record RegisterRequest(string Name, string Email, string Password, Gender? Gender);
public sealed record LoginRequest(string Email, string Password);
public sealed record RefreshRequest(string RefreshToken);
public sealed record LogoutRequest(string RefreshToken);