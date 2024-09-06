using MediatR;

namespace IdentityManager.Core.Roles.Commands.GrantAccessRight
{
    public sealed record GrantAccessRightCommand : IRequest<GrantAccessRightCommandResult>
    {
        public string RoleName { get; init; }
        public string AccessRightCode { get; init; }
    }
}
