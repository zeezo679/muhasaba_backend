// src/Muhasabaa.Application/Auth/Register/RegisterCommand.cs
using ErrorOr;
using MediatR;
using Muhasabaa.Application.Common.DTOs;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Application.Auth.Register;

public sealed record RegisterCommand(string Name, string Email, string Password, Gender? Gender) : IRequest<ErrorOr<AuthResult>>;

