using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Identities
{
    public sealed record IdentityBoundedWithExternalEntityEvent : DomainEvent
    {
        public Guid IdentityId { get; init; }
        public Guid ExternalId { get; init; }
    }

    public sealed record IdentityActivationPreparedEvent : DomainEvent
    {
        public Guid IdentityId { get; init; }
    }

    public sealed record IdentityActivatedEvent : DomainEvent
    {
        public Guid IdentityId { get; init; }
    }
}
