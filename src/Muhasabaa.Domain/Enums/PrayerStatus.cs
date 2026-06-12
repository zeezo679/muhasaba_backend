namespace Muhasabaa.Domain.Enums;

[Flags]
public enum PrayerStatus
{
    InJamaah = 1,
    AtHome = 2,
    Missed = 4,
}