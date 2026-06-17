using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Muhasabaa.Application.Common.Interfaces;
using Muhasabaa.Application.Common.Options;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Infrastructure.Services;

public class TokenService : ITokenService
{
    private readonly JwtOptions _jwtOptions;
    private readonly AppDbContext _dbContext;
    public TokenService(IOptions<JwtOptions> jwtOptions, AppDbContext dbContext)
    {
        _jwtOptions = jwtOptions.Value;
        _dbContext = dbContext;
    }

    public string GenerateAccessToken(ApplicationUser user)
    {
        //get claims of user
        var claims = BuildClaims(user);
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(1),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    //ErrorOr just for consistency
    public ErrorOr<RefreshToken> GenerateRefreshToken(Guid userId)
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        
        rng.GetBytes(randomNumber);

        var refreshToken = RefreshToken.CreateRefreshToken(
            token: Convert.ToBase64String(randomNumber),
            userId: userId,
            expiresAt: DateTime.UtcNow.AddDays(_jwtOptions.RefreshTokenLifetimeInDays)
        );

        return refreshToken;
    }

    public async Task<ErrorOr<Deleted>> RevokeTokenAsync(string token)
    {
        var refreshToken = await _dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token);

        if (refreshToken == null)
        {
            return Error.NotFound("Auth.TokenNotFound", "The provided refresh token does not exist.");
        }

        if (refreshToken.IsRevoked || refreshToken.IsExpired)
        {
            return Error.Unauthorized("Auth.TokenInvalid", "The token is already invalid.");
        }

        refreshToken.Revoke();
        await _dbContext.SaveChangesAsync();
    
        return Result.Deleted;
    }

    private IEnumerable<Claim> BuildClaims(ApplicationUser user)
    {
        
        if(user.Email is null)
            throw new InvalidOperationException("User email cannot be null when generating claims.");
        
        if(user.Gender is null)
            throw new InvalidOperationException("User gender cannot be null when generating claims.");
        
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Gender, user.Gender.ToString()!)
        };

        return claims;
    }
}