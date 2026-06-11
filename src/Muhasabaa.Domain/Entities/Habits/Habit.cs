using ErrorOr;
using Muhasabaa.Domain.Errors;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Domain.Entities.Habits;

public class Habit
{
    private Habit() { } // EF Core

    public int Id { get; private set; }
    public string NameAr { get; private set; } = string.Empty;
    public HabitCategory Category { get; private set; }
    public HabitInputType InputType { get; private set; }
    public string? Icon { get; private set; }
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; } = true;

    //the habits are seeded in the database, we might not even need the create method, but it's here for consistency and future-proofing
    public static ErrorOr<Habit> Create(
        string nameAr, 
        HabitCategory category, 
        HabitInputType inputType, 
        int sortOrder, 
        string? icon = null)
    {
        if (string.IsNullOrWhiteSpace(nameAr)) return HabitErrors.InvalidName;
        if (sortOrder < 1) return HabitErrors.InvalidSortOrder;
        if (!Enum.IsDefined(typeof(HabitCategory), category)) return HabitErrors.InvalidCategory;
        if (!Enum.IsDefined(typeof(HabitInputType), inputType)) return HabitErrors.InvalidInputType;

        return new Habit
        {
            NameAr = nameAr,
            Category = category,
            InputType = inputType,
            SortOrder = sortOrder,
            Icon = icon,
            IsActive = true
        };
    }

    public ErrorOr<Updated> UpdateDetails(string nameAr, HabitCategory category, string? icon)
    {
        if (string.IsNullOrWhiteSpace(nameAr)) return HabitErrors.InvalidName;
        if (!Enum.IsDefined(typeof(HabitCategory), category)) return HabitErrors.InvalidCategory;

        NameAr = nameAr;
        Category = category;
        Icon = icon;

        return Result.Updated;
    }

    public ErrorOr<Updated> SetSortOrder(int newSortOrder)
    {
        if (newSortOrder < 1) return HabitErrors.InvalidSortOrder;
        
        SortOrder = newSortOrder;
        return Result.Updated;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}