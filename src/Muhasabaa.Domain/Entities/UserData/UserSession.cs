namespace Muhasabaa.Domain.Entities.UserData;

public class UserSession
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime OpenedAt { get; private set; } = DateTime.UtcNow;
    public string? DeviceHint { get; private set; } // "mobile-web", "desktop-web" from user agent

    private UserSession() { } // for EF Core

    public static UserSession Create(Guid userId, string? deviceHint)
    {
        return new UserSession
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            OpenedAt = DateTime.UtcNow,
            DeviceHint = deviceHint
        };
    }
}