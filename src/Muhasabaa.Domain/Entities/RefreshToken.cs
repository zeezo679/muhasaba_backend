using Muhasabaa.Domain.Errors;

namespace Muhasabaa.Domain.Entities;
using ErrorOr;

public class RefreshToken
{
    public string Token {get; private set; } = string.Empty;
    public Guid UserId { get; private set; }
    public DateTime ExpiresAt { get;  set; }
    public bool IsRevoked { get; private set; } = false;
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    
    public bool IsActive => !IsRevoked && !IsExpired;
    
    public static ErrorOr<RefreshToken> CreateRefreshToken(string token, Guid userId, DateTime expiresAt)
    {
        if (string.IsNullOrWhiteSpace(token)) return RefreshTokenErrors.NullToken;
        if (userId == Guid.Empty) return RefreshTokenErrors.NullUserId;
        if (expiresAt <= DateTime.UtcNow) return RefreshTokenErrors.InvalidExpiration;

        return new RefreshToken
        {
            Token = token,
            UserId = userId,
            ExpiresAt = expiresAt,
            IsRevoked = false
        };
    }
}