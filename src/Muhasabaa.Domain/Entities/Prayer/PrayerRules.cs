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

    public static int MaximumScore = 145; //maximum score for a prayer log entry for a single prayer
    
    public static bool HasSunnah(PrayerName prayerName) => PrayersWithSunnah.Contains(prayerName);

    public static IEnumerable<PrayerStatus> GetAvailableStatuses(Gender? gender)
    {
        yield return PrayerStatus.AtHome;
        yield return PrayerStatus.Missed;
        
        if(gender == Gender.Male)
            yield return PrayerStatus.InJamaah;
    }
    
    public static int CalculatePrayerScore(PrayerName name, PrayerStatus status, bool prayedSunnah, Gender? gender)
    {
        if (status.HasFlag(PrayerStatus.Missed)) return 0;

        int score = 15;

        if (gender == Gender.Male && status.HasFlag(PrayerStatus.InJamaah))
            score += 10;
        
        if (prayedSunnah && HasSunnah(name))
            score += 5;

        return score;
    }
}