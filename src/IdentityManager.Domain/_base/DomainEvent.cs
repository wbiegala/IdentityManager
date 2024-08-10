using MediatR;

namespace IdentityManager.Domain.Base
{
    public abstract record DomainEvent : INotification
    {
        public Guid EventId { get; init; }
        public DateTimeOffset CreationTimestamp { get; init; }
    }
}
