using ErrorOr;

namespace Muhasabaa.Domain.Errors;

public static class HabitErrors
{
    public static readonly Error InvalidName = Error.Validation(
        code: "Habit.InvalidName",
        description: "Habit name cannot be empty.");

    public static readonly Error InvalidSortOrder = Error.Validation(
        code: "Habit.InvalidSortOrder",
        description: "Sort order must be greater than or equal to 1.");

    public static readonly Error InvalidCategory = Error.Validation(
        code: "Habit.InvalidCategory",
        description: "Invalid habit category.");

    public static readonly Error InvalidInputType = Error.Validation(
        code: "Habit.InvalidInputType",
        description: "Invalid habit input type.");
}

