using IdentityManager.Core.Base;

namespace IdentityManager.Core.Roles.Commands
{
    public sealed record CreateRoleCommandResult : CommandResult
    {
        public Guid? Id { get; init; }
    }
}
