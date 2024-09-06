using IdentityManager.Domain.Base;

namespace IdentityManager.Domain.Roles
{
    public class AccessRightAlreadyGrantedException : DomainException<Role>
    {
        public override string Message { get; }

        public AccessRightAlreadyGrantedException(Role role, string accessRightCode) : base(role)
        {
            Message = $"Role with name '{role.Name}' already has access right with code '{accessRightCode}'";
        }
    }

    public class AccessRightNotGrantedException : DomainException<Role>
    {
        public override string Message { get; }

        public AccessRightNotGrantedException(Role role, string accessRightCode) : base(role)
        {
            Message = $"Role with name '{role.Name}' has not access right with code '{accessRightCode}' granted";
        }
    }

    public class RoleIsActiveException : DomainException<Role>
    {
        public override string Message { get; }

        public RoleIsActiveException(Role role) : base(role)
        {
            Message = $"Role with name '{role.Name}' is active.";
        }
    }

    public class RoleIsInactiveException : DomainException<Role>
    {
        public override string Message { get; }

        public RoleIsInactiveException(Role role) : base(role)
        {
            Message = $"Role with name '{role.Name}' is inactive.";
        }
    }
}
