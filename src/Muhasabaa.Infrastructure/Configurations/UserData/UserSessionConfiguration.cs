using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhasabaa.Domain.Entities.UserData;
using Muhasabaa.Infrastructure.Identity.Models;

namespace Muhasabaa.Infrastructure.Configurations.UserData;

public class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSessions");
        
        builder.HasKey(us => us.Id);

        builder.Property(us => us.DeviceHint)
            .HasMaxLength(100);

        builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(us => us.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(us => new { us.UserId, us.OpenedAt });
    }
}
