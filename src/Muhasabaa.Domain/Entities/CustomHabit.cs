namespace Muhasabaa.Domain.Entities;

public class CustomHabit
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string NameAr { get; set; } = string.Empty;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User User { get; set; } = null!;
}