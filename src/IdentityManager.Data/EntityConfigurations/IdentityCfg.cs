using IdentityManager.Domain.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManager.Data.EntityConfigurations
{
    internal class IdentityCfg : IEntityTypeConfiguration<Identity>
    {
        public const int Name_MaxLength = 128;
        public const int Email_MaxLength = 256;

        public void Configure(EntityTypeBuilder<Identity> builder)
        {
            builder.HasKey(identity => identity.Id);

            builder.Ignore(identity => identity.DomainEvents);

            builder.Property(identity => identity.ExternalId);

            builder.Property(identity => identity.Email)
                .IsRequired()
                .HasMaxLength(Email_MaxLength);

            builder.HasIndex(identity => identity.Email)
                .IsUnique();

            builder.Property(identity => identity.Name)
                .IsRequired()
                .HasMaxLength(Name_MaxLength);

            builder.HasMany(identity => identity.Credentials)
                .WithOne(credential => credential.Identity);

            builder.HasMany(identity => identity.Roles)
                .WithOne(ir => ir.Identity);

            builder.Property(identity => identity.CreatedAt)
                .IsRequired();

            builder.Property(identity => identity.Status)
                .IsRequired()
                .HasConversion(value => value.ToString(), stored => Enum.Parse<IdentityStatus>(stored));
        }
    }
}
