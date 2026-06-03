using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Domain.Entities.Prayer;

public class PrayerLog
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public PrayerName PrayerName { get; private set; }
    public PrayerStatus Status { get; private set; }
    public DateOnly Date { get; private set; }
    public bool PrayedSunnah { get; private set; }
    public DateTime LoggedAt { get; private set; } = DateTime.UtcNow;
}