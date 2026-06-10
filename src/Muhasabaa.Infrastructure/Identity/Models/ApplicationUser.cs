using Microsoft.AspNetCore.Identity;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Habits;
using Muhasabaa.Infrastructure.Identity.Enums;

namespace Muhasabaa.Infrastructure.Identity.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Gender? Gender { get; set; }

    public ICollection<DailyLog> DailyLogs { get; set; } = [];
    public ICollection<CustomHabit> CustomHabits { get; set; } = [];
}