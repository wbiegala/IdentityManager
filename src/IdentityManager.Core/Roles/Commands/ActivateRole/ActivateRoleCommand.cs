using MediatR;

namespace IdentityManager.Core.Roles.Commands.ActivateRole
{
    public sealed record ActivateRoleCommand : IRequest<ActivateRoleCommandResult>
    {
        public string RoleName { get; init; }
    }
}
