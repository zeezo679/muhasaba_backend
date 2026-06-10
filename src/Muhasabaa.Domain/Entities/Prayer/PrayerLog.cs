using Muhasabaa.Domain.Enums;
using Muhasabaa.Domain.Errors;
using ErrorOr;

namespace Muhasabaa.Domain.Entities.Prayer;

public class PrayerLog
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    
    public PrayerName PrayerName { get; private set; }
    public PrayerStatus Status { get; private set; }
    public DateOnly Date { get; private set; }
    public int Score { get; private set; }
    public int MaximumScore { get; private set; }
    public bool PrayedSunnah { get; private set; }
    
    public DateTime LoggedAt { get; private set; } = DateTime.UtcNow;
    
    private PrayerLog() { } // for EF Core
    
    public static ErrorOr<PrayerLog> Create(Guid userId, PrayerName prayerName, PrayerStatus status, DateOnly date, Gender? gender, int score, int maximumScore, bool prayedSunnah, bool prayedQiyam)
    {
        if(status.HasFlag(PrayerStatus.InJamaah) && gender != Gender.Male) return PrayerLogErrors.InvalidGender;
        
        return new PrayerLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PrayerName = prayerName,
            Status = status,
            Date = date,
            Score = score,
            MaximumScore = maximumScore,
            PrayedSunnah = prayedSunnah,
            LoggedAt = DateTime.UtcNow
        };
    }
}