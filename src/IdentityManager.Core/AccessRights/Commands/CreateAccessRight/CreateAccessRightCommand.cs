using MediatR;

namespace IdentityManager.Core.AccessRights.Commands.CreateAccessRight
{
    public sealed record CreateAccessRightCommand : IRequest<CreateAccessRightCommandResult>
    {
        public string Code { get; init; }
        public string Name { get; init; }
        public string? Description { get; init; }
    }
}
