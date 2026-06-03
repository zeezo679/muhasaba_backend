namespace Muhasabaa.Domain.Entities;

public class UserSession
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime OpenedAt { get; set; } = DateTime.UtcNow;
    public string? DeviceHint { get; set; } // "mobile-web", "desktop-web" from user agent

}