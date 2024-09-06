using IdentityManager.Core.Base;

namespace IdentityManager.Core.AccessRights.Commands.CreateAccessRight
{
    public sealed record CreateAccessRightCommandResult : CommandResult
    {
        public Guid? Id { get; init; }
    }
}
