using MediatR;

namespace IdentityManager.Core.Roles.Commands
{
    public sealed record CreateRoleCommand : IRequest<CreateRoleCommandResult>
    {
        public Guid CreatorId { get; init; }
        public string Name { get; init; }
    }
}
