namespace Muhasabaa.Domain.Entities;

public class CustomHabitLog
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CustomHabitId { get; set; }
    public bool Completed { get; set; }
    public DateOnly Date { get; set; }
    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;
}