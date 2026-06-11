using ErrorOr;
using Muhasabaa.Domain.Errors;

namespace Muhasabaa.Domain.Entities.Habits;

public class CustomHabit
{
    private CustomHabit() { } // private constructor to enforce factory method usage
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    
    public string NameAr { get; private set; } = string.Empty;
    public int SortOrder { get; private set; }
    public bool IsActive { get; private set; } = true;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    
    
    public static ErrorOr<CustomHabit> Create(Guid userId, string nameAr, int sortOrder)
    {
        if (userId == Guid.Empty) return CustomHabitErrors.NullError;
        if (string.IsNullOrWhiteSpace(nameAr)) return CustomHabitErrors.NullError;
        if (sortOrder < 1) return CustomHabitErrors.InvalidSortOrder;
        
        return new CustomHabit
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            NameAr = nameAr,
            SortOrder = sortOrder,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }
    
    public ErrorOr<Updated> SetSortOrder(int newSortOrder)
    {
        if (newSortOrder < 1) return CustomHabitErrors.InvalidSortOrder;
        
        this.SortOrder = newSortOrder;
        return Result.Updated;
    }
    
    public ErrorOr<Updated> UpdateHabit(string nameAr)
    {
        if (string.IsNullOrWhiteSpace(nameAr)) return CustomHabitErrors.NullError;
        
        this.NameAr = nameAr;
        return Result.Updated;
    }
    
    public void Deactivate() => this.IsActive = false;
    public void Activate() => this.IsActive = true;
}