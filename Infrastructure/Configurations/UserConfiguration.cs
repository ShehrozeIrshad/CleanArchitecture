using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName).HasMaxLength(100).IsRequired();

            builder.Property(user => user.LastName).HasMaxLength(100).IsRequired();

            builder.Property(user => user.Email).HasMaxLength(250).IsRequired();

            builder.Property(user => user.Password).HasMaxLength(128).IsRequired();

            builder.Property(user => user.Device).IsRequired(false);

            builder.Property(user => user.IpAddress).HasMaxLength(15).IsRequired(false);

        }
    }
}
