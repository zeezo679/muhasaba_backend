using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhasabaa.Domain.Entities.Habits;

namespace Muhasabaa.Infrastructure.Configurations.Habits;

public class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.ToTable("Habits");
        
        builder.HasKey(h => h.Id);
        
        builder.Property(h => h.NameAr)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(h => h.Category)
            .HasConversion<int>();

        builder.Property(h => h.InputType)
            .HasConversion<int>();
        
        builder.HasIndex(h => new { h.Id, h.NameAr})
            .IsUnique();
    }
}