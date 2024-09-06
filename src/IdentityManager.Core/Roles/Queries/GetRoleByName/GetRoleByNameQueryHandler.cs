using IdentityManager.Data.Repositories;
using MediatR;

namespace IdentityManager.Core.Roles.Queries.GetRoleByName
{
    internal class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, GetRoleQueryResult?>
    {
        private readonly IRoleRepository _repository;

        public GetRoleByNameQueryHandler(IRoleRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<GetRoleQueryResult?> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetByNameAsync(request.Name, cancellationToken);

            return role is null ? null : role.Map();
        }
    }
}
