namespace Muhasabaa.Domain.Entities;

public class HabitScoring
{
    public const int DhikrMaxScore = 100;
    public const int QuranRecitationMaxScore = 100;
    public const int GymMaxScore = 100;
    public const int CustomHabitScore = 10;
    public const int ScreenTimePenaltyPerHour = 20;
    
    public static int CalculateDhikrScore(int count) => count >= 100 ? DhikrMaxScore : count; // 1 point per dhikr, up to 100
    public static int CalculateQuranRecitationScore(int pages) => pages >= 20 ? QuranRecitationMaxScore : pages * 5;
    public static int CalculateGymScore(int minutes) => minutes >= 20 ? GymMaxScore : minutes * 5;
    public static int CalculateScreenTimePenalty(int hours) => -(hours * ScreenTimePenaltyPerHour);
}