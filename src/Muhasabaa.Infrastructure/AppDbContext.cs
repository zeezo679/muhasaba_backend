using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Muhasabaa.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Habits;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>(options)
{
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
    
    public DbSet<ApplicationUser> Users => Set<ApplicationUser>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    public DbSet<UserSettings> UserSettings => Set<UserSettings>();
    public DbSet<DailyLog> DailyLogs => Set<DailyLog>();
    public DbSet<PrayerLog> PrayerLogs => Set<PrayerLog>();
    public DbSet<Habit> Habits => Set<Habit>();
    public DbSet<CustomHabit> CustomHabits => Set<CustomHabit>();
    public DbSet<CustomHabitLog> CustomHabitLogs => Set<CustomHabitLog>();
}