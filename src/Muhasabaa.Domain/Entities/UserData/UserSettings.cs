namespace Muhasabaa.Domain.Entities.UserData;

public class UserSettings
{
    public Guid UserId { get; private set; } //this is the pk
    public bool NotificationsEnabled { get; private set; } = false;
    public string Language { get; private set; } = "ar";

    private UserSettings() { } // for EF Core

    public static UserSettings Create(Guid userId, bool notificationsEnabled, string language)
    {
        return new UserSettings
        {
            UserId = userId,
            NotificationsEnabled = notificationsEnabled,
            Language = language
        };
    }
}