namespace Muhasabaa.Domain.Entities;

public class UserSettings
{
    public Guid UserId { get; set; } //this is the pk
    public bool NotificationsEnabled { get; set; } = false;
    public string Language { get; set; } = "ar";
}