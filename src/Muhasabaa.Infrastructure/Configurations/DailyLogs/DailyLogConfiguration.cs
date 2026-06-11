using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhasabaa.Domain.Entities.DailyLogs;
using Muhasabaa.Infrastructure.Identity.Models;

namespace Muhasabaa.Infrastructure.Configurations.DailyLogs;

public class DailyLogConfiguration : IEntityTypeConfiguration<DailyLog>
{
    public void Configure(EntityTypeBuilder<DailyLog> builder)
    {
        builder.ToTable("DailyLogs");
        
        builder.HasKey(dl => dl.Id);
        
        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(dl => dl.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasIndex(dl => new { dl.UserId, dl.Date })
            .IsUnique();
    }
}