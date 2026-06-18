// src/Muhasabaa.Application/Common/Interfaces/ITokenService.cs
using ErrorOr;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Application.Common.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser user);
    ErrorOr<RefreshToken> GenerateRefreshToken(Guid userId);
    Task<ErrorOr<Deleted>> RevokeTokenAsync(string token);
}


