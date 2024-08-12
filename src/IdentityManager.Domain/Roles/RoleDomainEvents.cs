using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Roles
{
    public sealed record RoleCreatedEvent : DomainEvent
    {
        public string Name { get; init; }
    }

    public sealed record RoleRenamedEvent : DomainEvent
    {
        public Guid RoleId { get; init; }
        public string OldName { get; init; }
        public string NewName { get; init; }
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
