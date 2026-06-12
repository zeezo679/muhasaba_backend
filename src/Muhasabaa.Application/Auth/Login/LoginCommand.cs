// src/Muhasabaa.Application/Auth/Login/LoginCommand.cs
using ErrorOr;
using MediatR;
using Muhasabaa.Application.Common.DTOs;

namespace Muhasabaa.Application.Auth.Login;

public sealed record LoginCommand(string Email, string Password) : IRequest<ErrorOr<AuthResult>>;

