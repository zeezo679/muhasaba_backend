// src/Muhasabaa.Application/Auth/Login/LoginCommandHandler.cs
using ErrorOr;
using MediatR;
using Muhasabaa.Application.Common.DTOs;
using Muhasabaa.Application.Common.Interfaces;

namespace Muhasabaa.Application.Auth.Login;

public sealed class LoginCommandHandler(IIdentityService identityService, ITokenService tokenService, IAppDbContext dbContext)
    : IRequestHandler<LoginCommand, ErrorOr<AuthResult>>
{
    public async Task<ErrorOr<AuthResult>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var userResult = await identityService.ValidateCredentialsAsync(request.Email, request.Password);
        
        if (userResult.IsError) 
            return userResult.Errors;

        var user = userResult.Value;
        var accessToken = tokenService.GenerateAccessToken(user);
        var refreshToken = tokenService.GenerateRefreshToken(user.Id).Value;
        
        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AuthResult(accessToken, refreshToken.Token, refreshToken.ExpiresAt);
    }
}

