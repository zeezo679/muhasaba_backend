namespace Muhasabaa.Domain.Enums;

[Flags]
public enum PrayerStatus
{
    None = 0,
    InJamaah = 1,
    AtHome = 2,
    Missed = 4,
}