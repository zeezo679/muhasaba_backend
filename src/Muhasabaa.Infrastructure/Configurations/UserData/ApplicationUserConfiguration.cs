using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Muhasabaa.Domain.Entities.UserData;

namespace Muhasabaa.Infrastructure.Configurations.UserData;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(u => u.Gender)
            .HasConversion<int>();
    }
}
