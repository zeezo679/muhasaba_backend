using ErrorOr;

namespace Muhasabaa.Domain.Errors;

public class CustomHabitErrors
{
    public static readonly Error NullError =
        Error.Validation("CustomHabit.EmptyAttribute", "required attribute is null or empty");
    
    public static readonly Error InvalidSortOrder =
        Error.Validation("CustomHabit.InvalidSortOrder", "sort order must be greater than 0");
}