namespace IdentityManager.Domain.Roles
{
    public abstract class RoleException : Exception { }

    public class AccessRightAlreadyGrantedException : RoleException
    {
        public override string Message { get; }

        public AccessRightAlreadyGrantedException(string roleName, string accessRightCode)
        {
            Message = $"Role with name '{roleName}' already has access right with code '{accessRightCode}'";
        }
    }

    public class AccessRightNotGrantedException : RoleException
    {
        public override string Message { get; }

        public AccessRightNotGrantedException(string roleName, string accessRightCode)
        {
            Message = $"Role with name '{roleName}' has not access right with code '{accessRightCode}' granted";
        }
    }
}
