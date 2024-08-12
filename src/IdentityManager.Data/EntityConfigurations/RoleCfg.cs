using IdentityManager.Domain.Roles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManager.Data.EntityConfigurations
{
    internal class RoleCfg : IEntityTypeConfiguration<Role>
    {
        public const string TableName = "Roles";

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable(TableName);

            builder.Ignore(role => role.DomainEvents);

            builder.HasKey(role => role.Id);

            builder.Property(role => role.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(role => role.Name)
                .IsUnique();

            builder.HasMany(role => role.AccessRights)
                .WithMany(accessRight => accessRight.Roles);

            builder.Property(role => role.CreatedAt)
                .IsRequired();

            builder.Property(role => role.ModifiedAt)
                .IsRequired();

            builder.Property(role => role.IsActive)
                .IsRequired();
        }
    }
}
