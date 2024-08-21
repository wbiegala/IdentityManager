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
            var role = await _repository.GetRoleByNameAsync(request.Name, cancellationToken);

            return role is null
                ? null
                : new()
                {
                    Id = role.Id,
                    Name = role.Name,
                    CreatedAt = role.CreatedAt,
                    ModifiedAt = role.ModifiedAt,
                    IsActive = role.IsActive
                };
        }
    }
}
