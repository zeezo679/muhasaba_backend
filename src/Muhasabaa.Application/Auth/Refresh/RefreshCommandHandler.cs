// src/Muhasabaa.Application/Auth/Refresh/RefreshCommandHandler.cs
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.UserData;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Application.Common.DTOs;

namespace Muhasabaa.Application.Auth.Refresh;

public sealed class RefreshCommandHandler(IAppDbContext dbContext, UserManager<ApplicationUser> userManager, ITokenService tokenService)
    : IRequestHandler<RefreshCommand, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(RefreshCommand request, CancellationToken cancellationToken)
    {
        var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.RefreshToken, cancellationToken);
        if (token is null || !token.IsActive)
        {
            return Error.Unauthorized();
        }

        var user = await userManager.FindByIdAsync(token.UserId.ToString());
        if (user is null)
        {
            return Error.Unauthorized();
        }

        var accessToken = tokenService.GenerateAccessToken(user);
        return new AuthResult(accessToken, token.Token, token.ExpiresAt);
    }
}

