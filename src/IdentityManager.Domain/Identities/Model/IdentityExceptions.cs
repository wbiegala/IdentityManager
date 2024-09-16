using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Identities
{
    public class IdentityHasNoActiveCredentialsException : DomainException<Identity>
    {
        public override string Message => $"Identity with id='{_aggregate.Id}' has no credentials.";

        public IdentityHasNoActiveCredentialsException(Identity identity) : base(identity) { }
    }

    public class IdentityIsActiveException : DomainException<Identity>
    {
        public override string Message => $"Identity with id='{_aggregate.Id}' is active.";

        public IdentityIsActiveException(Identity identity) : base(identity) { }
    }

    public class IdentityHasInvalidStatusException : DomainException<Identity>
    {
        private readonly string? _operation;
        public override string Message => $"Identity with id='{_aggregate.Id}' has invalid status ({_aggregate.Status}) for this operation ({_operation})";

        public IdentityHasInvalidStatusException(Identity identity, string? operation = null)
            : base(identity)
        {
            _operation = operation;
        }
    }

    public class IdentityAlreadyBoundWithExternalEntityException : DomainException<Identity>
    {
        public override string Message => $"Identity with id='{_aggregate.Id}' is aready bound with external entity id='{_aggregate.ExternalId}'";

        public IdentityAlreadyBoundWithExternalEntityException(Identity aggregate) : base(aggregate) { }
    }
}
