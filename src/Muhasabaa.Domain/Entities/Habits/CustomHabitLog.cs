namespace Muhasabaa.Domain.Entities.Habits;

public class CustomHabitLog
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid CustomHabitId { get; private set; }
    public bool Completed { get; private set; }
    public DateOnly Date { get; private set; }
    public DateTime LoggedAt { get; private set; } = DateTime.UtcNow;

    private CustomHabitLog() { } // for EF Core

    public static CustomHabitLog Create(Guid userId, Guid customHabitId, bool completed, DateOnly date)
    {
        return new CustomHabitLog
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CustomHabitId = customHabitId,
            Completed = completed,
            Date = date,
            LoggedAt = DateTime.UtcNow
        };
    }
}