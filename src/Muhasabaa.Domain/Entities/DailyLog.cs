namespace Muhasabaa.Domain.Entities;

public class DailyLog
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
    public int EarnedScore { get; set; }
    public int MaximumScore { get; set; }
    public int Percentage { get; set; }
    public DateTime CalculatedAt { get; set; }
}