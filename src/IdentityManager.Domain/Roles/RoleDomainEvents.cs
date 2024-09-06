using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Roles
{
    public sealed record RoleActivatedEvent : DomainEvent
    {
        public Guid RoleId { get; init; }
        public string RoleName { get; init; }
    }

    public sealed record RoleDeactivatedEvent : DomainEvent
    {
        public Guid RoleId { get; init; }
        public string RoleName { get; init; }
    }

    public sealed record AccessRightGrantedEvent : DomainEvent
    {
        public Guid RoleId { get; init; }
        public string AccessRightCode { get; init; }
    }

    public sealed record AccessRightRevokedEvent : DomainEvent
    {
        public Guid RoleId { get; init; }
        public string AccessRightCode { get; init; }
    }
}
