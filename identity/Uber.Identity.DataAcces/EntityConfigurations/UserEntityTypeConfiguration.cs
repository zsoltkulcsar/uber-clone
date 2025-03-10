using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Uber.Common.Entities;
using Uber.Common.Entities.Constants;
using Uber.Identity.Domain.Entities;

namespace Uber.Identity.DataAcces.EntityConfigurations
{
    internal sealed class UserEntityTypeConfiguration :
        BaseConfiguration<User>
    {
        protected override void ConfigureEntity(EntityTypeBuilder<User> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName(FiledNames.UserId);

            builder.Property(c => c.Name)
                .HasMaxLength(Sizes.NameMaxLength)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasMaxLength(Sizes.EmailMaxLength)
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasDefaultValueSql(DefaultValue.SysDateTimeOffset)
                .IsRequired();
        }
    }
}
