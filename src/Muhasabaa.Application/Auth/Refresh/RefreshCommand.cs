// src/Muhasabaa.Application/Auth/Refresh/RefreshCommand.cs
using ErrorOr;
using MediatR;
using Muhasabaa.Application.Common.DTOs;

namespace Muhasabaa.Application.Auth.Refresh;

public sealed record RefreshCommand(string RefreshToken) : IRequest<ErrorOr<AuthResult>>;

