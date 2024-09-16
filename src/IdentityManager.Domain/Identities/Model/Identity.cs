using IdentityManager.Domain.Base;
using IdentityManager.Domain.Shared;

namespace IdentityManager.Domain.Identities
{
    public class Identity : Aggregate
    {
        private readonly HashSet<Credential> _credentials = new();
        private readonly HashSet<IdentityRole> _roles = new();

        /// <summary>
        /// EF Core constructor
        /// </summary>
        private Identity() { }

        public Guid? ExternalId { get; private set; }
        public string Email { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<Credential> Credentials => _credentials;
        public IReadOnlyCollection<IdentityRole> Roles => _roles;
        public DateTimeOffset CreatedAt { get; private set; }
        public IdentityStatus Status { get; private set; }


        public static Identity Create(string email, string name, Guid? externalId = null, Credential? credential = null)
        {
            var identity = new Identity()
            {
                Email = email,
                Name = name,
                CreatedAt = TimestampGenerator.UtcNow,
                Status = IdentityStatus.Inactive,
            };

            if (externalId.HasValue)
                identity.ExternalId = externalId.Value;

            if (credential is not null)
                identity._credentials.Add(credential);

            return identity;
        }

        public void BindWithExternalEntity(Guid externalId)
        {
            if (ExternalId is null)
                throw new IdentityAlreadyBoundWithExternalEntityException(this);

            ExternalId = externalId;

            var @event = new IdentityBoundedWithExternalEntityEvent
            {
                EventId = Guid.NewGuid(),
                CreationTimestamp = TimestampGenerator.UtcNow,
                IdentityId = Id,
                ExternalId = ExternalId.Value
            };
            AddEvent(@event);
        }

        public void PrepareActivation()
        {
            if (!IsStatusValidForActivationPreparation())
                throw new IdentityHasInvalidStatusException(this, "ACTIVATION PREPARATION");

            Status = IdentityStatus.ReadyForActivation;

            var @event = new IdentityActivationPreparedEvent
            {
                EventId = Guid.NewGuid(),
                CreationTimestamp = TimestampGenerator.UtcNow,
                IdentityId = Id
            };
            AddEvent(@event);
        }

        public void Activate()
        {
            if (!IsStatusValidForActivation())
                throw new IdentityHasInvalidStatusException(this, "ACTIVATION");

            Status = IdentityStatus.Active;

            foreach (var credential in _credentials)
            {
                credential.Activate();
            }

            var @event = new IdentityActivatedEvent
            {
                EventId = Guid.NewGuid(),
                CreationTimestamp = TimestampGenerator.UtcNow,
                IdentityId = Id
            };
            AddEvent(@event);
        }

        private bool HasAnyActiveCredential() =>
            _credentials.Any(c => c.IsActive);

        private bool IsStatusValidForActivationPreparation() =>
            Status == IdentityStatus.Inactive || Status == IdentityStatus.Deactivated;

        private bool IsStatusValidForActivation() => 
            Status == IdentityStatus.ReadyForActivation;
    }
}
