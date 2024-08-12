using IdentityManager.Domain.AccessRights;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManager.Data.EntityConfigurations
{
    internal class AccessRightCfg : IEntityTypeConfiguration<AccessRight>
    {
        public const string TableName = "AccessRights";

        public void Configure(EntityTypeBuilder<AccessRight> builder)
        {
            builder.ToTable(TableName);

            builder.Ignore(accessRight => accessRight.DomainEvents);

            builder.HasKey(accessRight => accessRight.Id);

            builder.Property(accessRight => accessRight.Code)
                .IsRequired()
                .HasMaxLength(32);

            builder.HasIndex(accessRight => accessRight.Code).IsUnique();

            builder.Property(accessRight => accessRight.Name)
                .IsRequired()
                .HasMaxLength(128);

            builder.HasIndex(accessRight => accessRight.Name).IsUnique();

            builder.Property(accessRight => accessRight.Description)
                .HasMaxLength(512);

            builder.Property(accessRight => accessRight.CreatedAt)
                .IsRequired();

            builder.HasMany(accessRight => accessRight.Roles)
                .WithMany(role => role.AccessRights);
        }
    }
}
