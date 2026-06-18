// src/Muhasabaa.Application/Auth/Logout/LogoutCommandHandler.cs
using ErrorOr;
using MediatR;
using Muhasabaa.Application.Common.Interfaces;

namespace Muhasabaa.Application.Auth.Logout;

public sealed class LogoutCommandHandler(ITokenService tokenService)
    : IRequestHandler<LogoutCommand, ErrorOr<Deleted>>
{
    public Task<ErrorOr<Deleted>> Handle(LogoutCommand request, CancellationToken cancellationToken)
        => tokenService.RevokeTokenAsync(request.RefreshToken);
}

