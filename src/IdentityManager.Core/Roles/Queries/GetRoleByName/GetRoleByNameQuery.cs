using MediatR;

namespace IdentityManager.Core.Roles.Queries.GetRoleByName
{
    public sealed record GetRoleByNameQuery(string Name) : IRequest<GetRoleQueryResult>;
}
