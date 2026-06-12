// src/Muhasabaa.Application/Auth/Register/RegisterCommandHandler.cs
using ErrorOr;
using MediatR;
using Muhasabaa.Application.Common.DTOs;
using Muhasabaa.Application.Common.Interfaces;

namespace Muhasabaa.Application.Auth.Register;

public sealed class RegisterCommandHandler(IIdentityService identityService, ITokenService tokenService, IAppDbContext dbContext)
    : IRequestHandler<RegisterCommand, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var userResult = await identityService.CreateUserAsync(request.Name, request.Email, request.Password, request.Gender);
        if (userResult.IsError)
        {
            return userResult.Errors;
        }

        var user = userResult.Value;
        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = await tokenService.GenerateRefreshTokenAsync(user.Id);

        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AuthResult(accessToken, refreshToken.Token, refreshToken.ExpiresAt);
    }
}

