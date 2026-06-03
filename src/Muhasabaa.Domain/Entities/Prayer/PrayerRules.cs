using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Domain.Entities.Prayer;

public static class PrayerRules
{
    private static readonly HashSet<PrayerName> PrayersWithSunnah = [
        PrayerName.Fajr,
        PrayerName.Dhuhr,
        PrayerName.Maghrib,
        PrayerName.Isha
    ];
    
    public static bool HasSunnah(PrayerName prayerName) => PrayersWithSunnah.Contains(prayerName);
}