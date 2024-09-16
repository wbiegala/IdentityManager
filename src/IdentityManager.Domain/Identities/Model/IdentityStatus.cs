namespace IdentityManager.Domain.Identities
{
    public enum IdentityStatus
    {
        Inactive = 0,
        Deactivated = 1,
        ReadyForActivation = 5,
        Active = 10,
        Banned = 99
    }
}
