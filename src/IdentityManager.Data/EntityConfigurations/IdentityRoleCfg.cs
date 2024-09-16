using IdentityManager.Domain.Identities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityManager.Data.EntityConfigurations
{
    internal class IdentityRoleCfg : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasKey(ir => ir.Id);

            builder.HasOne(ir => ir.Role)
                .WithMany();

            builder.Property(ir => ir.ValidFrom)
                .IsRequired();

            builder.Property(ir => ir.ValidTo);
        }
    }
}
