using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uber.Identity.Domain.Entities;

namespace Uber.Identity.DataAcces.EntityConfigurations
{
    internal sealed class UserEntityTypeConfiguration :
        BaseConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
            .IsUnique();

            modelBuilder.Entity<User>()
                .Property(u => u.Role)
            .HasConversion<string>();

            modelBuilder.Entity<User>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        }
    }
}
