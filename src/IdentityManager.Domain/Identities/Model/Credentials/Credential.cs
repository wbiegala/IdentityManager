using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Identities
{
    public abstract class Credential : Entity
    {        
        public Identity Identity { get; private set; }
        public DateTimeOffset CreatedAt { get; private set; }
        public bool IsActive { get; private set; }

        public void Activate()
        {
            IsActive = true;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}
