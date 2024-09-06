using MediatR;

namespace IdentityManager.Core.AccessRights.Commands.DeleteAccessRight
{
    public sealed record DeleteAccessRightCommand : IRequest<DeleteAccessRightCommandResult>
    {
        public string Code { get; init; }
    }
}
