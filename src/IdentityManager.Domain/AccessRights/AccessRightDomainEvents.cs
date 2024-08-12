using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.AccessRights
{
    public sealed record AccessRightCreatedEvent : DomainEvent
    {
        public string Code { get; init; }
        public string Name { get; init; }
    }
}
