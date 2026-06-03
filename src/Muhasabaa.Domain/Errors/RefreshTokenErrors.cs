using ErrorOr;

namespace Muhasabaa.Domain.Errors;

public class RefreshTokenErrors
{
    public static readonly Error NullToken =
        Error.Validation("RefreshToken.NullToken", "refresh token cannot be null or empty");
    
    public static readonly Error NullUserId =
        Error.Validation("RefreshToken.NullUserId", "user ID cannot be empty");
    
    public static readonly Error InvalidExpiration =
        Error.Validation("RefreshToken.InvalidExpiration", "expiration time must be in the future");
}