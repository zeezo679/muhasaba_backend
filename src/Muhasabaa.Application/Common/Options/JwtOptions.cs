namespace Muhasabaa.Application.Common.Options;

public class JwtOptions
{
    public static string SectionName = "JwtSettings";
    public string Secret { get; set; } = null!;
    public string Issuer { get; set; } = string.Empty;
    public string Audience { get; set; } = string.Empty;
    public int TokenLifetimeInMinutes { get; set; }
    public int RefreshTokenLifetimeInDays { get; set; }
}