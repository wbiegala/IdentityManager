using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Identities
{
    public record CredentialPassword : ValueObject
    {
        public const string Name = "Password";

        public string Value { get; private set; }

        internal CredentialPassword Create(string login) =>
            new() { Value = login };
    }
}
