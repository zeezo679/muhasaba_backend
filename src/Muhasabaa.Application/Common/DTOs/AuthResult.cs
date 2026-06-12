// src/Muhasabaa.Application/Common/DTOs/AuthResult.cs
namespace Muhasabaa.Application.Common.DTOs;

public sealed record AuthResult(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiresAt);

