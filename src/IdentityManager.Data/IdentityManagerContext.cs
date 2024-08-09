using Microsoft.EntityFrameworkCore;

namespace IdentityManager.Data
{
    public class IdentityManagerContext : DbContext, IUnitOfWork
    {
        public IdentityManagerContext(DbContextOptions options)
            : base(options)
        {
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            // implementation of domain events publication

            await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityManagerContext).Assembly);
        }
    }
}
