using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Domain.Entities;

public class DailyScoreCalculator
{
    public DailyScore Calculate(
        IEnumerable<PrayerLog> prayers,
        int dhikrCount,
        int quranPages,
        int gymMinutes,
        int screenTimeHours,
        IEnumerable<CustomHabitLog> customHabits,
        Gender? gender)
    {
        int maximum = PrayerRules.MaximumScore
                      + HabitScoring.DhikrMaxScore
                      + HabitScoring.QuranRecitationMaxScore
                      + HabitScoring.GymMaxScore
                      + customHabits.Count() * HabitScoring.CustomHabitScore;

        int earned = prayers.Sum(p => PrayerRules.CalculatePrayerScore(p.PrayerName, p.Status, p.PrayedSunnah, gender))
                     + HabitScoring.CalculateDhikrScore(dhikrCount)
                     + HabitScoring.CalculateQuranRecitationScore(quranPages)
                     + HabitScoring.CalculateGymScore(gymMinutes)
                     - HabitScoring.CalculateScreenTimePenalty(screenTimeHours)
                     + customHabits.Count(h => h.Completed) * HabitScoring.CustomHabitScore;

        earned = Math.Max(0, earned);
        
        int percentage = maximum == 0 ? 0 : (int)Math.Round((double)earned / maximum * 100);

        return new DailyScore(earned, maximum, percentage);
    }
}