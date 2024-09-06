using MediatR;

namespace IdentityManager.Core.Roles.Commands.DeactivateRole
{
    public sealed record DeactivateRoleCommand : IRequest<DeactivateRoleCommandResult>
    {
        public string RoleName { get; init; }
    }
}
