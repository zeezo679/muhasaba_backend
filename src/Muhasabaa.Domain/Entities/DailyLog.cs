namespace Muhasabaa.Domain.Entities;

public class DailyLog
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }

    // one of these is set depending on the habit
    public int? HabitId { get; set; }
    public Guid? CustomHabitId { get; set; }

    public bool? BoolValue { get; set; }   // checkboxes
    public decimal? NumericValue { get; set; } // pages, hours, minutes

    public DateTime LoggedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
    public Habit? Habit { get; set; }
    public CustomHabit? CustomHabit { get; set; }
}