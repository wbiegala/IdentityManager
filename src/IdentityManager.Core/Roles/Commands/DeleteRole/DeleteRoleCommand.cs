using MediatR;

namespace IdentityManager.Core.Roles.Commands.DeleteRole
{
    public sealed record DeleteRoleCommand : IRequest<DeleteRoleCommandResult>
    {
        public string RoleName { get; init; }
        public bool ForceDelete { get; init; }
    }
}
