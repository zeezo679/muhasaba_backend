// src/Muhasabaa.Application/Auth/Logout/LogoutCommand.cs
using ErrorOr;
using MediatR;

namespace Muhasabaa.Application.Auth.Logout;

public sealed record LogoutCommand(string RefreshToken) : IRequest<ErrorOr<Deleted>>;

