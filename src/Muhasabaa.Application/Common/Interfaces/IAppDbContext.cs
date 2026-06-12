// src/Muhasabaa.Application/Common/Interfaces/IAppDbContext.cs
using Microsoft.EntityFrameworkCore;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Domain.Entities.Habits;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<ApplicationUser> Users { get; }
    DbSet<RefreshToken> RefreshTokens { get; }
    DbSet<UserSession> UserSessions { get; }
    DbSet<UserSettings> UserSettings { get; }
    DbSet<DailyLog> DailyLogs { get; }
    DbSet<PrayerLog> PrayerLogs { get; }
    DbSet<Habit> Habits { get; }
    DbSet<CustomHabit> CustomHabits { get; }
    DbSet<CustomHabitLog> CustomHabitLogs { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

