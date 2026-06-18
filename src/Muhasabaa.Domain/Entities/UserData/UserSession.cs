namespace Muhasabaa.Domain.Entities.UserData;

public class UserSession
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime OpenedAt { get; private set; } = DateTime.UtcNow;
    public string? DeviceHint { get; private set; } // "mobile-web", "desktop-web" from user agent
    public bool IsRevoked { get; private set; } = false;
    public DateTime? LastActivityAt { get; private set; }
    public DateTime? LoggedOutAt { get; private set; }

    private UserSession() { } // for EF Core

    public static UserSession Create(Guid userId, string? deviceHint)
    {
        return new UserSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            DeviceHint = deviceHint,
            LastActivityAt = DateTime.UtcNow
        };
    }
}