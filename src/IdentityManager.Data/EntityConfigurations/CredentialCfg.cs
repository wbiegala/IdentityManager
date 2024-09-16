using IdentityManager.Domain.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManager.Data.EntityConfigurations
{
    internal class CredentialCfg : IEntityTypeConfiguration<Credential>
    {
        public static string LoginAndPasswordCredentialType = "LoginAndPassword";

        public void Configure(EntityTypeBuilder<Credential> builder)
        {
            builder.HasKey(credential => credential.Id);

            builder.Property(credential => credential.CreatedAt)
                .IsRequired();

            builder.Property(credential => credential.IsActive)
                .IsRequired();

            builder.HasDiscriminator<string>("CredentialType")
                .HasValue<LoginAndPasswordCredential>(LoginAndPasswordCredentialType);
        }
    }

    internal class LoginAndPasswordCredentialCfg : IEntityTypeConfiguration<LoginAndPasswordCredential>
    {
        public void Configure(EntityTypeBuilder<LoginAndPasswordCredential> builder)
        {
            builder.OwnsOne(credential => credential.Login, ctx =>
            {
                ctx.Property(login => login.Value)
                    .HasColumnName(CredentialLogin.Name)
                    .IsRequired();

                ctx.HasIndex(login => login.Value)
                    .IsUnique();
            });

            builder.OwnsOne(credential => credential.Password, ctx =>
            {
                ctx.Property(password => password.Value)
                    .HasColumnName(CredentialPassword.Name)
                    .IsRequired();
            });
        }
    }
}
