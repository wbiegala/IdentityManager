using IdentityManager.Domain.Base;
using IdentityManager.Domain.Roles;

namespace IdentityManager.Domain.AccessRights
{
    public class AccessRight : Aggregate
    {
        private readonly HashSet<Role> _assignedRoles = new();

        /// <summary>
        /// EF Core constructor
        /// </summary>
        private AccessRight() { }

        private AccessRight(string code, string name, string? description, DateTimeOffset timestamp)
        {
            Code = code;
            Name = name;
            Description = description;
            CreatedAt = timestamp;

            var @event = new AccessRightCreatedEvent
            {
                EventId = Guid.NewGuid(),
                CreationTimestamp = timestamp,
                Code = code,
                Name = name,
            };
            AddEvent(@event);
        }

        /// <summary>
        /// Short code of AccessRight. Must be unique. This code is load to JWT payload.
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// Full name of AccessRight
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Description of Access Right
        /// </summary>
        public string? Description { get; private set; }

        /// <summary>
        /// Collection of roles that access role is granted
        /// </summary>
        public IReadOnlyCollection<Role> Roles => _assignedRoles;

        /// <summary>
        /// Creation timestamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        public bool IsGrantedToAnyRole() 
            => _assignedRoles.Any();

        public static AccessRight Create(string code, string name, string? description, DateTimeOffset timestamp) =>
            new AccessRight(code, name, description, timestamp);
    }
}
