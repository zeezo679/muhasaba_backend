namespace Muhasabaa.Domain.Entities.DailyLogs;
using ErrorOr;

public class DailyLog
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public DateOnly Date { get; private set; }
    
    //Computed data
    public int EarnedScore { get; private set; }
    public int MaximumScore { get; private set; }
    public int Percentage { get; private set; }
    
    // Raw data for analysis
    public int DhikrCount { get; private set; }
    public int QuranPages { get; private set; }
    public int GymMinutes { get; private set; }
    public int ScreenTimeHours { get; private set; }
    public bool PrayedQiyam { get; private set; }
    public int SleepHours { get; private set; }
    public int DeepWorkHours { get; private set; }
    public DateTime CalculatedAt { get; private set; }


    private DailyLog() { } // for EF Core

    public static ErrorOr<DailyLog> Create(Guid userId, DateOnly date, int earnedScore, int maximumScore, int sleepHours, int deepWorkHours)
    {
        if (earnedScore < 0) return Error.Failure("EarnedScore must be non-negative.");
        if (maximumScore <= 0) return Error.Failure("MaximumScore must be greater than zero.");
        if (earnedScore > maximumScore) return Error.Failure("EarnedScore cannot exceed MaximumScore.");
        if (sleepHours < 0 || sleepHours > 24) return Error.Failure("SleepHours must be between 0 and 24.");
        if (deepWorkHours < 0 || deepWorkHours > 24) return Error.Failure("DeepWorkHours must be between 0 and 24.");

        int percentage = (int)Math.Round((double)earnedScore / maximumScore * 100);

        return new DailyLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Date = date,
            EarnedScore = earnedScore,
            MaximumScore = maximumScore,
            Percentage = percentage,
            SleepHours = sleepHours,
            DeepWorkHours = deepWorkHours,
            CalculatedAt = DateTime.UtcNow
        };
    }
}