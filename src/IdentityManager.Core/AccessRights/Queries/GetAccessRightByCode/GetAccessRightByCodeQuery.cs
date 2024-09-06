using MediatR;

namespace IdentityManager.Core.AccessRights.Queries.GetAccessRightByCode
{
    public sealed record GetAccessRightByCodeQuery(string Code) : IRequest<GetAccessRightQueryResult?>;
}
