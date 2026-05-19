namespace Muhasabaa.Domain.Entities;

public class UserSettings
{
    public Guid UserId { get; set; }
    public bool NotificationsEnabled { get; set; } = false;
    public string Language { get; set; } = "ar";
}