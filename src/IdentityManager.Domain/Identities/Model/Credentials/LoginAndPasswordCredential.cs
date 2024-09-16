namespace IdentityManager.Domain.Identities
{
    public class LoginAndPasswordCredential : Credential
    {
        public CredentialLogin Login { get; private set; }
        public CredentialPassword Password { get; private set; }
    }
}
