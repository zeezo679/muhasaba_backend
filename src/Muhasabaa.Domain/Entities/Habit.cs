using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Domain.Entities;

public class Habit
{
    public int Id { get; set; }
    public string NameAr { get; set; } = string.Empty;
    public HabitCategory Category { get; set; }
    public HabitInputType InputType { get; set; }
    public string? Icon { get; set; }
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}