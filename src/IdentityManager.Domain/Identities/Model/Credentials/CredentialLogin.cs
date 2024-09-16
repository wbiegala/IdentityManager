using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Identities
{
    public sealed record CredentialLogin : ValueObject
    {
        public const string Name = "Login";

        public string Value { get; private set; }

        internal CredentialLogin Create(string login) =>
            new() { Value = login };
    }
}
