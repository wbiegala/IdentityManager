using IdentityManager.Domain.Base;
using IdentityManager.Domain.Roles;

namespace IdentityManager.Domain.Identities
{
    public class IdentityRole : Entity
    {
        public Role Role { get; private set; }
        public Identity Identity { get; private set; }
        public DateTimeOffset ValidFrom { get; private set; }
        public DateTimeOffset? ValidTo { get; private set; }
    }
}
