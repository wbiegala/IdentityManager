using IdentityManager.Domain.Base;
using System.Data;

namespace IdentityManager.Domain.Roles
{
    public class AccessRightAlreadyGrantedException : DomainException<Role>
    {
        private readonly string _accessRightCode;

        public override string Message => $"Role with name '{_aggregate.Name}' already has access right with code '{_accessRightCode}'";

        public AccessRightAlreadyGrantedException(Role role, string accessRightCode) : base(role)
        {
            _accessRightCode = accessRightCode;
        }
    }

    public class AccessRightNotGrantedException : DomainException<Role>
    {
        private readonly string _accessRightCode;

        public override string Message => $"Role with name '{_aggregate.Name}' has not access right with code '{_accessRightCode}' granted";

        public AccessRightNotGrantedException(Role role, string accessRightCode) : base(role)
        {
            _accessRightCode = accessRightCode;
        }
    }

    public class RoleIsActiveException : DomainException<Role>
    {
        public override string Message => $"Role with name '{_aggregate.Name}' is active.";

        public RoleIsActiveException(Role role) : base(role) { }
    }

    public class RoleIsInactiveException : DomainException<Role>
    {
        public override string Message => $"Role with name '{_aggregate.Name}' is inactive.";

        public RoleIsInactiveException(Role role) : base(role) { }
    }
}
