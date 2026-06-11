using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhasabaa.Domain.Entities.Habits;

namespace Muhasabaa.Infrastructure.Configurations.Habits;

public class CustomHabitLogConfiguration : IEntityTypeConfiguration<CustomHabitLog>
{
    public void Configure(EntityTypeBuilder<CustomHabitLog> builder)
    {
        builder.ToTable("CustomHabitLogs");
        
        builder.HasKey(chl => chl.Id);
        
        builder.HasOne<CustomHabit>()
            .WithMany()
            .HasForeignKey(chl => chl.CustomHabitId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(chl => new { chl.UserId, chl.CustomHabitId, chl.Date })
            .IsUnique();
    }
}