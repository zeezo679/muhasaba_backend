using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhasabaa.Domain.Entities.Habits;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Infrastructure.Configurations.Habits;

public class CustomHabitConfiguration : IEntityTypeConfiguration<CustomHabit>
{
    public void Configure(EntityTypeBuilder<CustomHabit> builder)
    {
        builder.ToTable("CustomHabits");
        
        builder.HasKey(ch => ch.Id);
        
        builder.Property(ch => ch.NameAr)
            .IsRequired()
            .HasMaxLength(100);
        
        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(ch => ch.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(ch => new { ch.UserId, ch.NameAr })
            .IsUnique();
    }
}