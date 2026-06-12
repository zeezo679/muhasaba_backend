using Microsoft.AspNetCore.Identity;
using Muhasabaa.Domain.Enums;

namespace Muhasabaa.Domain.Entities.UserData;

public class ApplicationUser : IdentityUser<Guid>
{
    public string Name { get; set; } = string.Empty;
    public string? ProfileImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Gender? Gender { get; set; }
}

