using IdentityManager.Domain.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace IdentityManager.Data
{
    public class IdentityManagerContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediator;

        public IdentityManagerContext(IMediator mediator, DbContextOptions options)
            : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken = default)
        {
            var aggregateType = typeof(Aggregate);
            foreach (var entry in ChangeTracker.Entries())
            {
                if (aggregateType.IsAssignableFrom(entry.Entity.GetType()))
                {
                    var entity = entry.Entity as Aggregate;
                    var events = entity?.DomainEvents;

                    if (events != null && events.Any())
                    {
                        await Task.WhenAll(events.Select(@event => _mediator.Publish(@event, cancellationToken)));
                        entity?.ClearEvents();
                    }
                }
            }

            await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityManagerContext).Assembly);
        }
    }
}
