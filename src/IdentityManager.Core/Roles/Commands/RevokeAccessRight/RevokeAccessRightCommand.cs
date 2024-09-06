using MediatR;

namespace IdentityManager.Core.Roles.Commands.RevokeAccessRight
{
    public sealed record RevokeAccessRightCommand : IRequest<RevokeAccessRightCommandResult>
    {
        public string RoleName { get; init; }
        public string AccessRightCode { get; init; }
    }
}
