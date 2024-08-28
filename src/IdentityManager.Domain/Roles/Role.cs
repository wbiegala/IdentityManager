﻿using IdentityManager.Domain.AccessRights;
using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Roles
{
    public class Role : Aggregate
    {
        #region consts

        public const int MaxLength_Name = 128;

        #endregion


        private readonly HashSet<AccessRight> _accessRights = new();

        /// <summary>
        /// EF Core constructor
        /// </summary>
        private Role() { }

        private Role(string name, DateTimeOffset timestamp)
        {
            Name = name;
            CreatedAt = timestamp;
            ModifiedAt = timestamp;
            IsActive = true;
        }

        /// <summary>
        /// Role name. Must be unique.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Collection of granted AccessRights
        /// </summary>
        public IReadOnlyCollection<AccessRight> AccessRights => _accessRights;

        /// <summary>
        /// Creation timestamp
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Last time modification timestamp
        /// </summary>
        public DateTimeOffset ModifiedAt { get; private set; }

        /// <summary>
        /// Flag determines if role is active and can be granted to any identity and used for JWT
        /// </summary>
        public bool IsActive { get; private set; }

        public void GrantAccessRight(AccessRight accessRight, DateTimeOffset timestamp)
        {
            if (_accessRights.Any(ar => ar.Id == accessRight.Id))
                throw new AccessRightAlreadyGrantedException(this.Name, accessRight.Code);

            _accessRights.Add(accessRight);

            var @event = new AccessRightGrantedEvent
            {
                EventId = Guid.NewGuid(),
                CreationTimestamp = timestamp,
                RoleId = Id,
                AccessRightCode = accessRight.Code,
            };
            AddEvent(@event);
        }

        public void RevokeAccessRight(AccessRight accessRight, DateTimeOffset timestamp)
        {
            if (!_accessRights.Any(ar => ar.Id == accessRight.Id))
                throw new AccessRightNotGrantedException(this.Name, accessRight.Code);

            _accessRights.RemoveWhere(ar => ar.Id == accessRight.Id);

            var @event = new AccessRightRevokedEvent
            {
                EventId = Guid.NewGuid(),
                CreationTimestamp = timestamp,
                RoleId = Id,
                AccessRightCode = accessRight.Code,
            };
            AddEvent(@event);
        }

        public void Deactivate(DateTimeOffset timestamp)
        {
            IsActive = false;

            var @event = new RoleDeactivatedEvent
            {
                EventId= Guid.NewGuid(),
                CreationTimestamp= timestamp,
                RoleId = Id,
                RoleName = Name,
            };
            AddEvent(@event);
        }

        public static Role Create(string name, DateTimeOffset timestamp) =>
            new Role(name, timestamp);
    }
}
