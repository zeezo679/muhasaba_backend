using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhasabaa.Domain.Entities.Prayer;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Infrastructure.Configurations.Prayer;

public class PrayerLogConfiguration : IEntityTypeConfiguration<PrayerLog>
{
    public void Configure(EntityTypeBuilder<PrayerLog> builder)
    {
        builder.ToTable("PrayerLogs");
        
        builder.HasKey(pl => pl.Id);
        
        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(pl => pl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(pl => new { pl.UserId, pl.Date, pl.PrayerName })
            .IsUnique();
    }
}