using ErrorOr;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Infrastructure.Services;

public class TokenService : ITokenService
{
    public string GenerateAccessToken(ApplicationUser user)
        => throw new NotImplementedException();

    public Task<RefreshToken> GenerateRefreshTokenAsync(Guid userId)
        => throw new NotImplementedException();

    public Task<ErrorOr<Deleted>> RevokeTokenAsync(string token)
        => throw new NotImplementedException();
}